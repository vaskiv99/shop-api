using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopService.Common.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleting { get; set; }

        public List<GoodsCategory> GoodsCategories { get; set; } = new List<GoodsCategory>();
    }
}
