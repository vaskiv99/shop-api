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
    public class CategoryQueryHandler : BaseQueryHandler,
        IRequestHandler<GetCategoryById,CategoryWithGoodsResponse>,
        IRequestHandler<GetCategories, QueryResult<CategoryResponse>>
    {
        private readonly ShopContext _shopContext;

        public CategoryQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<CategoryWithGoodsResponse> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var category = await _shopContext.Categories.Include(x => x.GoodsCategories).ThenInclude(x => x.Goods)
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleting, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if(category == null)
                throw new DomainException(ErrorType.CategoryDoesNotExist);

            return category.ToResponseWithGoodsResponse();
        }

        public async Task<QueryResult<CategoryResponse>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, bool>> deletedFilter = x => !x.IsDeleting;
            Expression<Func<Category, bool>> searchFilter = x => (string.IsNullOrWhiteSpace(request.SearchQuery)) ||
                x.Name.ToUpper().Contains(request.SearchQuery.ToUpper());

            var count = await _shopContext.Categories
                .Where(deletedFilter)
                .Where(searchFilter)
                .CountAsync(cancellationToken).ConfigureAwait(false);

            if(count == 0)
                return new QueryResult<CategoryResponse>();

            var categories = await _shopContext.Categories
                .Where(deletedFilter)
                .Where(searchFilter)
                .AddPaging(request)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            var response = categories.Select(x => x.ToResponse()).ToList();
            var paging = CreatePaging(request, count);

            return new QueryResult<CategoryResponse>(response,paging);
        }
    }
}
