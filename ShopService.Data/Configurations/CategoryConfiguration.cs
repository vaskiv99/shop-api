using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Common.Models;

namespace ShopService.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).HasMaxLength(255);
            modelBuilder.HasIndex(x => x.IsDeleting);
            modelBuilder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
