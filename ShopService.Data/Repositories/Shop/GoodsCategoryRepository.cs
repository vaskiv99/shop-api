using ShopService.Common.Enums;
using ShopService.Common.Models;
using ShopService.Data.Db;

namespace ShopService.Data.Repositories.Shop
{
    public class GoodsCategoryRepository : GeneralRepository<GoodsCategory, long>
    {
        public GoodsCategoryRepository(ShopContext context) : base(context, context.GoodsCategories)
        {
        }

        protected override ErrorType NotExist => ErrorType.GoodsCategoryDoesNotExist;
    }
}
