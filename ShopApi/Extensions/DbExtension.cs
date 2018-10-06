using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Data.Db;
using ShopService.Data.Services;

namespace ShopService.Web.Extensions
{
    public static class DbExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration,
            bool useInMemoryDb = false)
        {
            if (useInMemoryDb) return;

            var connection = configuration.GetConnectionString("PostgresConnection");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ShopContext>(options =>
                {
                    options.UseNpgsql(connection, p => p.MigrationsAssembly("ShopService.Web"));
                    options.ConfigureWarnings(p => p.Log());
                });
        }

        public static void MigrateDatabase(this IServiceProvider provider)
        {
            var databaseInitializer = provider.GetService<DatabaseInitializer>();
            databaseInitializer.Migrate();
        }
    }
}
