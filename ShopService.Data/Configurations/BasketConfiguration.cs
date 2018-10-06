using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Common.Models;

namespace ShopService.Data.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> modelBuilder)
        {
            modelBuilder.HasKey(x => new {x.UserId, x.GoodsId});
            modelBuilder.HasOne(x => x.Goods)
                .WithMany(x => x.Baskets)
                .HasForeignKey(x => x.GoodsId);

            modelBuilder.HasOne(x => x.User)
                .WithMany(x => x.Baskets)
                .HasForeignKey(x => x.UserId);
        }
    }
}
