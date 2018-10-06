using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Data.Db;

namespace ShopService.Web.Helpers
{
    public static class MigrateMiddleware
    {
        public static void AutoUpdateDataBase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ShopContext>().Database.Migrate();
            }
        }
    }
}
