using Microsoft.Extensions.DependencyInjection;
using ShopService.Data.Db;
using ShopService.Data.Repositories.Shop;
using ShopService.Services.Handlers;

namespace ShopService.Web.Extensions
{
    public static class DiExtension
    {
        public static void AddDiExtension(this IServiceCollection services)
        {
            services.AddScoped<ShopContext>();
            services.AddScoped<BasketRepository>();
            services.AddScoped<GoodsCategoryRepository>();
            services.AddScoped<GoodsRepository>();
            services.AddScoped<AccountCommandHandler>();
            services.AddScoped<BasketCommandHandler>();
            services.AddScoped<BasketQueryHandler>();
            services.AddScoped<CategoryCommandHandler>();
            services.AddScoped<CategoryQueryHandler>();
            services.AddScoped<GoodsCommandHandler>();
            services.AddScoped<GoodsQueryHandlers>();
            services.AddScoped<CategoryRepository>();
        }
    }
}
