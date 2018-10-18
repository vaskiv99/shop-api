using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopService.IdentityServer4;
using ShopService.Services.Commands;
using ShopService.Services.Responses;
using ShopService.Web.Helpers;

namespace ShopService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseCtrl
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region commands

        [ValidateModel]
        [HttpPost("Admin/Register")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCommand command)
        {
            command.Role = ServerRoles.Admin;
            var result = await _mediator.Send(command).ConfigureAwait(false);

            return JsonResult(result);
        }

        [ValidateModel]
        [HttpPost("Customer/Register")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCommand command)
        {
            command.Role = ServerRoles.Customer;
            var result = await _mediator.Send(command).ConfigureAwait(false);

            return JsonResult(result);
        }

        [ValidateModel]
        [HttpPost("Token")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var json = await _mediator.Send(command);

            return JsonResult(json);
        }

        #endregion

    }
}