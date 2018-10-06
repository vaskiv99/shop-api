using System.Collections.Generic;
using ShopService.Common.Enums;

namespace ShopService.Services.Responses
{
    public class GoodsResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Currency Currency { get; set; }

        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
    }
}
