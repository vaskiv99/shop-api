using MediatR;
using ShopService.Common.Infrastructure;
using ShopService.Services.Responses;

namespace ShopService.Services.Query
{
    public class GetCategoryById : IRequest<CategoryWithGoodsResponse>
    {
        public long Id { get; set; }
    }

    public class GetCategories : PageQuery, IRequest<QueryResult<CategoryResponse>>
    {
        public string SearchQuery { get; set; }
    }
}
