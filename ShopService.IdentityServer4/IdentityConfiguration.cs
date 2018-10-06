using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace ShopService.IdentityServer4
{
    public class IdentityConfiguration
    {
        private readonly IConfiguration _configuration;

        public IdentityConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource {
                    Name = "ShopApi",
                    DisplayName ="Shop API",
                    Description = "Shop API Access",
                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("ShopApiSecret".Sha256())},
                    Scopes = new List<Scope> {
                        new Scope("ShopApi.read"),
                        new Scope("ShopApi.write")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "shopClient",
                    ClientName = "Client Credentials Client Application",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("ShopApiSecret".Sha256())
                    },
                    AllowedScopes = new List<string> { "ShopApi.read" }
                }
            };
        }
    }
}
