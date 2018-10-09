using System;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Common.Models;
using ShopService.Data.Db;
using ShopService.IdentityServer4;

namespace ShopService.Web.Extensions
{
    public static class IdentityServerExtension
    {
        public static void AddIdentityServerConfig(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            //var authority = env.IsDevelopment() ? configuration["ID4:Authority"] : Environment.GetEnvironmentVariable("IDENTITY_SERVER_AUTHORITY");

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ShopContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityConfiguration.GetClients())
                .AddAspNetIdentity<User>();

            services.AddTransient<IProfileService, IdentityProfileService>();

            services.Configure<IdentityOptions>(config =>
            {
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["ID4:Authority"];
                    //options.Authority = "http://shopapi_web_1:8080/";
                    options.Audience = configuration["ID4:Audience"];
                    options.RequireHttpsMetadata = false;
                });
        }
    }
}
