using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Common.Infrastructure;
using ShopService.Common.Models;
using ShopService.Data.Db;
using ShopService.Services.Adapters;
using ShopService.Services.Query;
using ShopService.Services.Responses;

namespace ShopService.Services.Handlers
{
    public class GoodsQueryHandlers : BaseQueryHandler,
        IRequestHandler<GetGoodsById, GoodsResponse>,
        IRequestHandler<GetGoodsQuery, QueryResult<GoodsResponse>>
    {
        private readonly ShopContext _context;

        public GoodsQueryHandlers(ShopContext context)
        {
            _context = context;
        }

        public async Task<GoodsResponse> Handle(GetGoodsById request, CancellationToken cancellationToken)
        {
            var goods = await _context.Goods.Include(x => x.GoodsCategories).ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (goods == null)
                throw new DomainException(ErrorType.GoodsDoesNotExist);

            return goods.ToResponse();
        }

        public async Task<QueryResult<GoodsResponse>> Handle(GetGoodsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Goods, bool>> searchFilter = x => (string.IsNullOrWhiteSpace(request.SearchQuery)) ||
                                                                 x.Name.ToUpper().Contains(request.SearchQuery.ToUpper());

            var count = await _context.Goods.Where(searchFilter)
                .CountAsync(cancellationToken).ConfigureAwait(false);

            if (count == 0)
                return new QueryResult<GoodsResponse>();

            var goods = await _context.Goods.Where(searchFilter).AddPaging(request)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            var response = goods.Select(x => x.ToResponse()).ToList();
            var paging = CreatePaging(request, count);

            return new QueryResult<GoodsResponse>(response,paging);
        }
    }
}
