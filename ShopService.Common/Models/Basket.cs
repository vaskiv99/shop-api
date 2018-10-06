using System.ComponentModel.DataAnnotations.Schema;

namespace ShopService.Common.Models
{
    public class Basket
    {
        [ForeignKey(nameof(GoodsId))]
        public Goods Goods { get; set; }
        
        public long GoodsId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string UserId { get; set; }

        public long Count { get; set; }
    }
}
