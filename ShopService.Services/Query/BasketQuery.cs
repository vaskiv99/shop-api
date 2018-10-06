using MediatR;
using Newtonsoft.Json;
using ShopService.Services.Responses;

namespace ShopService.Services.Query
{
    public class GetBasketByUserId : IRequest<UserBasketResponse>
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public string SearchQuery { get; set; }
    }
}
