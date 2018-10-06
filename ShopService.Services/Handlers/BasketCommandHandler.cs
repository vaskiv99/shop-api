using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Data.Db;
using ShopService.Data.Repositories.Shop;
using ShopService.Services.Adapters;
using ShopService.Services.Commands;

namespace ShopService.Services.Handlers
{
    public class BasketCommandHandler : IRequestHandler<AddItemToBasket, bool>,
        IRequestHandler<DeleteItemFromBasket, bool>
    {
        private readonly BasketRepository _basketRepository;
        private readonly ShopContext _context;

        public BasketCommandHandler(BasketRepository basketRepository, ShopContext context)
        {
            _basketRepository = basketRepository;
            _context = context;
        }

        public async Task<bool> Handle(AddItemToBasket request, CancellationToken cancellationToken)
        {
            var basket = await _context.Baskets
                .FirstOrDefaultAsync(x => x.GoodsId == request.GoodsId && x.UserId == request.UserId, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (basket == null)
            {
                var item = request.ToModel();
                _basketRepository.Create(item);
            }

            else
            {
                basket.Count += request.Count;
            }

            await _basketRepository.SaveAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> Handle(DeleteItemFromBasket request, CancellationToken cancellationToken)
        {
            var basket =
                (await _basketRepository.FindAsync(x => x.GoodsId == request.GoodsId && x.UserId == request.UserId)).Data;

            if (basket.Count < request.Count)
                throw new DomainException(ErrorType.BasketItemCountLessThenNeed);

            if(basket.Count == request.Count)
                _basketRepository.Delete(basket);

            else
            {
                basket.Count -= request.Count;
                _basketRepository.Update(basket);
            }

            await _basketRepository.SaveAsync().ConfigureAwait(false);

            return true;
        }
    }
}
