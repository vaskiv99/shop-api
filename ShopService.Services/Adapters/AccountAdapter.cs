using ShopService.Common.Models;
using ShopService.Services.Commands;
using ShopService.Services.Responses;

namespace ShopService.Services.Adapters
{
    public static class AccountAdapter
    {
        public static User ToModel(this RegisterCommand command) => new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            UserName = command.UserName
        };

        public static UserResponse ToResponse(this User user) => new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName
        };
    }
}
