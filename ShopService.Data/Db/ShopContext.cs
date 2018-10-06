using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Models;
using ShopService.Data.Configurations;

namespace ShopService.Data.Db
{
    public class ShopContext : IdentityDbContext<User>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GoodsConfiguration());
            builder.ApplyConfiguration(new GoodsCategoryConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new BasketConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<GoodsCategory> GoodsCategories { get; set; }

        public DbSet<Basket> Baskets { get; set; }
    }
}
