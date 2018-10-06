using System.ComponentModel.DataAnnotations.Schema;

namespace ShopService.Common.Models
{
    public class GoodsCategory
    {
        public long GoodsId { get; set; }

        [ForeignKey(nameof(GoodsId))]
        public Goods Goods { get; set; }

        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
