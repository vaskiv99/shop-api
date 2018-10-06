using ShopService.Common.Enums;
using ShopService.Common.Models;
using ShopService.Data.Db;

namespace ShopService.Data.Repositories.Shop
{
    public class BasketRepository : GeneralRepository<Basket,long>
    {
        public BasketRepository(ShopContext context) : base(context, context.Baskets)
        {
        }

        protected override ErrorType NotExist => ErrorType.BasketDoesNotExist;
    }
}
