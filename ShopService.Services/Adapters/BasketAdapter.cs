using ShopService.Common.Models;
using ShopService.Services.Commands;

namespace ShopService.Services.Adapters
{
    public static class BasketAdapter
    {
        public static Basket ToModel(this AddItemToBasket item) => new Basket
        {
            GoodsId = item.GoodsId,
            UserId = item.UserId,
            Count = item.Count
        };
    }
}
