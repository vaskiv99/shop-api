using System.Linq;

namespace ShopService.Services.Query
{
    public static class BaseQueryBuilder
    {
        public static IQueryable<T> AddPaging<T>(this IQueryable<T> query, PageQuery pagging)
        {
            return query
                .Skip(pagging.PageSize * pagging.PageIndex)
                .Take(pagging.PageSize);
        }
    }
}
