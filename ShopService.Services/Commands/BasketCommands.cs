using MediatR;
using Newtonsoft.Json;

namespace ShopService.Services.Commands
{
    public class AddItemToBasket : IRequest<bool>
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public long GoodsId { get; set; }

        public long Count { get; set; }
    }

    public class DeleteItemFromBasket : AddItemToBasket
    {

    }
}
