using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopService.Services.Responses;

namespace ShopService.Services.Commands
{
    public class RegisterCommand : IRequest<UserResponse>
    {
        [JsonIgnore]
        public string Role { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class LoginCommand : IRequest<JObject>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
