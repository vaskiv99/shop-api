using System.Linq;
using ShopService.Common.Models;
using ShopService.Services.Commands;
using ShopService.Services.Responses;

namespace ShopService.Services.Adapters
{
    public static class CategoryAdapter
    {
        public static Category ToModel(this CreateCategoryCommand command) => new Category
        {
            Name = command.Name,
            IsDeleting = false
        };

        public static CategoryResponse ToResponse(this Category category) => new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };

        public static CategoryWithGoodsResponse ToResponseWithGoodsResponse(this Category category) =>
            new CategoryWithGoodsResponse
            {
                Id = category.Id,
                Name = category.Name,
                Goods = category.GoodsCategories.Select(x => x.Goods.ToResponse()).ToList()
            };
    }
}
