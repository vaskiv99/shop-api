using System.Collections.Generic;

namespace ShopService.Services.Responses
{
    public class CategoryResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryWithGoodsResponse : CategoryResponse
    {
        public List<GoodsResponse> Goods = new List<GoodsResponse>();
    }
}
