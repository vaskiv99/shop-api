using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopService.Data.Repositories.Shop;
using ShopService.Services.Adapters;
using ShopService.Services.Commands;
using ShopService.Services.Responses;

namespace ShopService.Services.Handlers
{
    public class GoodsCommandHandler : IRequestHandler<CreateGoodsCommand,GoodsResponse>,
        IRequestHandler<UpdateGoodsCommand,GoodsResponse>,
        IRequestHandler<DeleteGoodsCommand,bool>
    {
        private readonly GoodsRepository _goodsRepository;
        private readonly CategoryRepository _categoryRepository;

        public GoodsCommandHandler(GoodsRepository goodsRepository, CategoryRepository categoryRepository)
        {
            _goodsRepository = goodsRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<GoodsResponse> Handle(CreateGoodsCommand request, CancellationToken cancellationToken)
        {
            var validate = request.CategoryIds.Select(async x =>
                await _categoryRepository.ExistAsync(c => c.Id == x && !c.IsDeleting));

            await Task.WhenAll(validate);

            var goods = request.ToModel();

            _goodsRepository.Create(goods);
            await _goodsRepository.SaveAsync().ConfigureAwait(false);

            var response = (await _goodsRepository.GetWithIncludeAsync(goods.Id).ConfigureAwait(false)).Data.ToResponse();

            return response;
        }

        public async Task<GoodsResponse> Handle(UpdateGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = (await _goodsRepository.FindAsync(x => x.Id == request.Id).ConfigureAwait(false)).Data;

            var validate = request.CategoryIds.Select(async x =>
                await _categoryRepository.ExistAsync(c => c.Id == x && !c.IsDeleting));

            await Task.WhenAll(validate);

            goods.Edit(request);

            _goodsRepository.Update(goods);
            await _goodsRepository.SaveAsync().ConfigureAwait(false);

            var response = (await _goodsRepository.GetWithIncludeAsync(goods.Id).ConfigureAwait(false)).Data.ToResponse();

            return response;
        }

        public async Task<bool> Handle(DeleteGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = (await _goodsRepository.FindAsync(x => x.Id == request.Id).ConfigureAwait(false)).Data;

            _goodsRepository.Delete(goods);
            await _goodsRepository.SaveAsync().ConfigureAwait(false);

            return true;
        }
    }
}
