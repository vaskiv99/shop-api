using ShopService.Common.Infrastructure;
using ShopService.Services.Query;

namespace ShopService.Services.Handlers
{
    public class BaseQueryHandler
    {
        protected BaseQueryHandler()
        {
        }

        private static long DetermineNumberOfPages(int pageSize, long numberOfRecords)
        {
            if (pageSize == 0) return 0;
            return numberOfRecords / pageSize + (numberOfRecords % pageSize > 0 ? 1 : 0);
        }

        protected static Paging CreatePaging(PageQuery query, long numberOfRecords)
        {
            var numberOfPages = DetermineNumberOfPages(query.PageSize, numberOfRecords);
            return new Paging()
            {
                NumberOfRecords = numberOfRecords,
                NumberOfPages = numberOfPages,
                PageIndex = query.PageIndex,
            };
        }
    }
}
