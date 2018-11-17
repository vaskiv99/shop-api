using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Common.Models;
using ShopService.Data.Db;
using ShopService.Services.Adapters;
using ShopService.Services.Commands;
using ShopService.Services.Responses;

namespace ShopService.Services.Handlers
{
    public class AccountCommandHandler : IRequestHandler<RegisterCommand, UserResponse>,
        IRequestHandler<LoginCommand, JObject>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShopContext _context;
        private readonly string _url;

        public AccountCommandHandler(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ShopContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _url = configuration["ID4:Authority"];
        }

        public async Task<UserResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _context.Users.AnyAsync(x => x.Email == request.Email || x.UserName == request.UserName,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (isExist)
                throw new DomainException(ErrorType.DuplicatedEmailOrUserName);

            var user = request.ToModel();

            var result = await _userManager.CreateAsync(user, request.Password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(request.Role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(request.Role));
                }

                await _userManager.AddToRoleAsync(user, request.Role);

                await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
                await _userManager.AddClaimAsync(user, new Claim("firstName", user.FirstName));
                await _userManager.AddClaimAsync(user, new Claim("lastName", user.LastName));
                await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
                await _userManager.AddClaimAsync(user, new Claim("role", request.Role));
            }

            return user.ToResponse();
        }

        public async Task<JObject> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var disco = await DiscoveryClient.GetAsync(_url);
            var tokenClient = new TokenClient(disco.TokenEndpoint, "shopClient", "ShopApiSecret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(request.UserName, request.Password, "ShopApi.read",
                cancellationToken: cancellationToken);

            return tokenResponse.Json;
        }
    }
}
