using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Enums;
using ShopService.Common.Infrastructure;
using ShopService.Common.Interfaces;
using ShopService.Data.Db;

namespace ShopService.Data.Repositories
{
    public abstract class GeneralRepository<T, TK> where T : class
    {
        protected readonly ShopContext Db;
        protected readonly DbSet<T> DbSet;

        protected GeneralRepository(ShopContext context, DbSet<T> dataSet)
        {
            Db = context;
            DbSet = dataSet;
        }

        protected abstract ErrorType NotExist { get; }

        public async Task<IOperationResult<bool>> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await DbSet.AnyAsync(predicate);
            if (result)
                return new OperationResult<bool>(true);
            return new OperationResult<bool>(NotExist);
        }

        public void Delete(TK id)
        {
            var item = DbSet.Find(id);
            if (item != null)
                DbSet.Remove(item);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(T item)
        {
            if (item == null) return;
            Db.Entry(item).State = EntityState.Modified;
        }

        public void Update(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (item == null)
                    return;
                Db.Entry(item).State = EntityState.Modified;
            }
        }

        public void Create(params T[] items)
        {
            DbSet.AddRange(items);
        }

        public void UpdateRange(ICollection<T> items)
        {
            if (items.Count == 0) return;
            foreach (var item in items)
            {
                Db.Entry(item).State = EntityState.Modified;
            }
        }

        public async Task<bool> SaveAsync()
        {
            var changes = Db.ChangeTracker.Entries()
                .Count(p => p.State == EntityState.Modified || p.State == EntityState.Deleted ||
                            p.State == EntityState.Added);
            if (changes == 0) return true;
            return await Db.SaveChangesAsync() > 0;
        }

        public virtual async Task<IOperationResult<T>> GetAsync(TK id)
        {
            var result = await DbSet.FindAsync(id);
            return WrapResult(result);
        }

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var result = await DbSet.Where(expression).ToListAsync();
            return result;
        }

        public async Task<IOperationResult<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await DbSet.FirstOrDefaultAsync(predicate);
            return WrapResult(result);
        }

        public IOperationResult<T> WrapResult(T value)
        {
            if (value == null)
                return new OperationResult<T>(NotExist);
            return new OperationResult<T>(value);
        }
    }
}
