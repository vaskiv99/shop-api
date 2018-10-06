using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Common.Models;

namespace ShopService.Data.Configurations
{
    public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).HasMaxLength(255);
            modelBuilder.HasIndex(x => x.Name);
        }
    }
}
