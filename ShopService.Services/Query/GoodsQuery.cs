using MediatR;
using ShopService.Common.Infrastructure;
using ShopService.Services.Responses;

namespace ShopService.Services.Query
{
    public class GetGoodsById : IRequest<GoodsResponse>
    {
        public long Id { get; set; }
    }

    public class GetGoodsQuery : PageQuery, IRequest<QueryResult<GoodsResponse>>
    {
        public string SearchQuery { get; set; }
    }
}
