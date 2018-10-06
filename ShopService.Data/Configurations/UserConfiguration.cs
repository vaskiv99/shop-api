using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Common.Models;

namespace ShopService.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.Property(x => x.FirstName).HasMaxLength(255);
            modelBuilder.Property(x => x.LastName).HasMaxLength(255);

            modelBuilder.HasIndex(x => x.Email).IsUnique();
            modelBuilder.HasIndex(x => x.UserName).IsUnique();
        }
    }
}
