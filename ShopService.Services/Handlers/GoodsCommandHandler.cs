using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Data.Repositories.Shop;
using ShopService.Services.Adapters;
using ShopService.Services.Commands;
using ShopService.Services.Responses;

namespace ShopService.Services.Handlers
{
    public class GoodsCommandHandler : IRequestHandler<CreateGoodsCommand, GoodsResponse>,
        IRequestHandler<UpdateGoodsCommand, GoodsResponse>,
        IRequestHandler<DeleteGoodsCommand, bool>
    {
        private readonly GoodsRepository _goodsRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly GoodsCategoryRepository _goodsCategoryRepository;

        public GoodsCommandHandler(GoodsRepository goodsRepository, 
            CategoryRepository categoryRepository,
            GoodsCategoryRepository goodsCategoryRepository)
        {
            _goodsRepository = goodsRepository;
            _categoryRepository = categoryRepository;
            _goodsCategoryRepository = goodsCategoryRepository;
        }

        public async Task<GoodsResponse> Handle(CreateGoodsCommand request, CancellationToken cancellationToken)
        {
            var validate = request.CategoryIds.Select(async x =>
            {
                if (!(await _categoryRepository.ExistAsync(c => c.Id == x && !c.IsDeleting)).Data)
                    throw new DomainException(ErrorType.CategoryDoesNotExist);
            });

            await Task.WhenAll(validate);

            var goods = request.ToModel();

            _goodsRepository.Create(goods);
            await _goodsRepository.SaveAsync().ConfigureAwait(false);

            var response = (await _goodsRepository.GetWithIncludeAsync(goods.Id).ConfigureAwait(false)).Data.ToResponse();

            return response;
        }

        public async Task<GoodsResponse> Handle(UpdateGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = (await _goodsRepository.GetWithIncludeAsync(request.Id).ConfigureAwait(false)).Data;

            if (goods == null)
                throw new DomainException(ErrorType.GoodsDoesNotExist);

            var validate = request.CategoryIds.Select(async x =>
            {
                if (!(await _categoryRepository.ExistAsync(c => c.Id == x && !c.IsDeleting)).Data)
                    throw new DomainException(ErrorType.CategoryDoesNotExist);
            });

            await Task.WhenAll(validate);
            goods.GoodsCategories.ForEach(x => _goodsCategoryRepository.Delete(x));
            await _goodsCategoryRepository.SaveAsync().ConfigureAwait(false);
            goods.Edit(request);
            _goodsRepository.Update(goods);
            await _goodsRepository.SaveAsync().ConfigureAwait(false);

            var response = (await _goodsRepository.GetWithIncludeAsync(goods.Id).ConfigureAwait(false)).Data.ToResponse();

            return response;
        }

        public async Task<bool> Handle(DeleteGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = (await _goodsRepository.FindAsync(x => x.Id == request.Id).ConfigureAwait(false)).Data;

            if (goods == null)
                throw new DomainException(ErrorType.GoodsDoesNotExist);

            _goodsRepository.Delete(goods);
            await _goodsRepository.SaveAsync().ConfigureAwait(false);

            return true;
        }
    }
}
