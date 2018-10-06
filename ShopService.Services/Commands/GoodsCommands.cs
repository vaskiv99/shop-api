using System.Collections.Generic;
using MediatR;
using ShopService.Common.Enums;
using ShopService.Services.Responses;

namespace ShopService.Services.Commands
{
    public class CreateGoodsCommand : IRequest<GoodsResponse>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Currency Currency { get; set; }

        public List<long> CategoryIds { get; set; } = new List<long>();
    }

    public class UpdateGoodsCommand : CreateGoodsCommand
    {
        public long Id { get; set; }
    }

    public class DeleteGoodsCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
