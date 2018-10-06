using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Enums;
using ShopService.Common.Interfaces;
using ShopService.Common.Models;
using ShopService.Data.Db;

namespace ShopService.Data.Repositories.Shop
{
    public class CategoryRepository : GeneralRepository<Category, long>
    {
        public CategoryRepository(ShopContext context, DbSet<Category> dataSet) : base(context, dataSet)
        {
        }

        protected override ErrorType NotExist => ErrorType.CategoryDoesNotExist;

        public virtual async Task<IOperationResult<Category>> GetWithIncludeAsync(Expression<Func<Category,bool>> expression)
        {
            var result = await DbSet
                .Include(x => x.GoodsCategories)
                .FirstOrDefaultAsync(expression);
            return WrapResult(result);
        }
    }
}
