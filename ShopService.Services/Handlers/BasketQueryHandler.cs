using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopService.Data.Db;
using ShopService.Services.Adapters;
using ShopService.Services.Query;
using ShopService.Services.Responses;

namespace ShopService.Services.Handlers
{
    public class BasketQueryHandler : BaseQueryHandler,
        IRequestHandler<GetBasketByUserId, UserBasketResponse>
    {
        private readonly ShopContext _context;

        public BasketQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<UserBasketResponse> Handle(GetBasketByUserId request, CancellationToken cancellationToken)
        {
            var userBasket = await _context.Users.Include(x => x.Baskets).ThenInclude(x => x.Goods)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken).ConfigureAwait(false);

            return userBasket.ToBasketResponse();
        }
    }
}
