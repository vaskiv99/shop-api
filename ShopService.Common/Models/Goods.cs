using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopService.Common.Enums;

namespace ShopService.Common.Models
{
    public class Goods
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Currency Currency { get; set; }

        public List<GoodsCategory> GoodsCategories { get; set; } = new List<GoodsCategory>();

        public List<Basket> Baskets { get; set; } = new List<Basket>();
    }
}
