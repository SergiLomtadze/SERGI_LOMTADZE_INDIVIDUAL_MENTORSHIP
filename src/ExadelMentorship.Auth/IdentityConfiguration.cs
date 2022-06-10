using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace ExadelMentorship.Auth
{
    public class IdentityConfiguration
    {
        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1000",
                    Username = "admin",
                    Password = "adminpassword",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Admin"),
                    }
                },
                new TestUser
                {
                    SubjectId = "1001",
                    Username = "user",
                    Password = "userpassword",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "User"),
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("WebApi.read"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("WebApi")
                {
                    Scopes = new List<string>{ "WebApi.read"},
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "swagger.client",
                    ClientName = "Swagger Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "WebApi.read" }
                },
            };
    }
}
