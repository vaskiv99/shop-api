using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Enums;
using ShopService.Common.Interfaces;
using ShopService.Common.Models;
using ShopService.Data.Db;

namespace ShopService.Data.Repositories.Shop
{
    public class GoodsRepository : GeneralRepository<Goods, long>
    {
        public GoodsRepository(ShopContext context) : base(context, context.Goods)
        {
        }

        protected override ErrorType NotExist => ErrorType.GoodsDoesNotExist;

        public void AddRange(IEnumerable<Goods> range)
        {
            DbSet.AddRange(range);
        }

        public virtual async Task<IOperationResult<Goods>> GetWithIncludeAsync(long id)
        {
            var result = await DbSet
                .Include(x => x.GoodsCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            return WrapResult(result);
        }
    }
}
