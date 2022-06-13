using ExadelMentorship.Auth.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ExadelMentorship.Auth
{
    public class IdentityConfiguration
    {
        private IdentityConfig _identityConfig;
        public IdentityConfiguration(IOptions<IdentityConfig> identityConfig)
        {
            _identityConfig = identityConfig.Value;

        }
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("WebApi")
            {
                Scopes = { "WebApi.read", "role" },
                UserClaims = { "role" }
            }
        };

        public static IEnumerable<ApiScope> Scopes => new[]
        {
            new ApiScope("WebApi.read"),
            new ApiScope("role")
        };

        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "api-swagger",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "WebApi.read", "role" },
                RedirectUris = {"https://localhost:7066/swagger/oauth2-redirect.html"},
                RequireClientSecret = false,
                RequirePkce = true,
                AllowedCorsOrigins = { "https://localhost:7066" },
            }
        };

        public static IEnumerable<TestUser> TestUsers => new[]
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "admin",
                Password = "adminpassword",
                Claims =
                {
                    new Claim("given_name", "AdminName"),
                    new Claim("last_name", "AdminLastName"),
                    new Claim(JwtClaimTypes.Email, "admin@test.com"),
                    new Claim(JwtClaimTypes.Role, "Admin")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "user",
                Password = "userpassword",
                Claims =
                {
                    new Claim("given_name", "User"),
                    new Claim("last_name", "UserLastName"),
                    new Claim(JwtClaimTypes.Email, "user@test.com"),
                    new Claim(JwtClaimTypes.Role, "User")
                }
            }
        };
    }
}
