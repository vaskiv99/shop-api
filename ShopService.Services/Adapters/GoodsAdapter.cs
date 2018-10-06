using System.Linq;
using ShopService.Common.Models;
using ShopService.Services.Commands;
using ShopService.Services.Responses;

namespace ShopService.Services.Adapters
{
    public static class GoodsAdapter
    {
        public static Goods ToModel(this CreateGoodsCommand command) => new Goods
        {
            Name = command.Name,
            Currency = command.Currency,
            Price = command.Price,
            GoodsCategories = command.CategoryIds.Select(x => x.ToModel()).ToList()
        };

        public static GoodsCategory ToModel(this long id) => new GoodsCategory
        {
            CategoryId = id
        };

        public static GoodsResponse ToResponse(this Goods goods) => new GoodsResponse
        {
            Id = goods.Id,
            Name = goods.Name,
            Currency = goods.Currency,
            Price = goods.Price,
            Categories = goods.GoodsCategories?.Select(x => x.Category?.ToResponse()).ToList()
        };

        public static void Edit(this Goods goods, UpdateGoodsCommand command)
        {
            goods.Name = command.Name;
            goods.Currency = command.Currency;
            goods.Price = command.Price;
            goods.GoodsCategories = command.CategoryIds.Select(x => new GoodsCategory()
            {
                CategoryId = x,
                GoodsId = command.Id
            }).ToList();
        }

        public static UserBasketResponse ToBasketResponse(this User user) => new UserBasketResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Items = user.Baskets.Select(x => x.ToItem()).ToList()
        };

        public static Item ToItem(this Basket basket) => new Item
        {
            Goods = basket.Goods.ToResponse(),
            Count = basket.Count
        };
    }
}
