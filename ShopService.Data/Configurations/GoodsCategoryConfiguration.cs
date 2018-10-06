using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Common.Models;

namespace ShopService.Data.Configurations
{
    public class GoodsCategoryConfiguration : IEntityTypeConfiguration<GoodsCategory>
    {
        public void Configure(EntityTypeBuilder<GoodsCategory> modelBuilder)
        {
            modelBuilder.HasKey(x => new {x.CategoryId, x.GoodsId});

            modelBuilder.HasOne(x => x.Goods)
                .WithMany(x => x.GoodsCategories)
                .HasForeignKey(x => x.GoodsId);

            modelBuilder.HasOne(x => x.Category)
                .WithMany(x => x.GoodsCategories)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
