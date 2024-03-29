﻿using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace ExadelMentorship.Auth
{
    public class IdentityConfiguration
    {
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
            new ApiScope("role"),
            new ApiScope("mailApi" , "mail sending API"),
        };

        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "api-swagger",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "WebApi.read", "role" },
                RedirectUris = {ConfigurationHelper.config.GetSection("RedirectUris").Value },
                RequireClientSecret = false,
                RequirePkce = true,
                AllowedCorsOrigins = {ConfigurationHelper.config.GetSection("AllowedCorsOrigins").Value},
            },

            new Client
            {
              ClientId = "reportJob",
              AllowedGrantTypes = GrantTypes.ClientCredentials,
              ClientSecrets =
              {
                new Secret(ConfigurationHelper.config.GetSection("ReportJobSecret").Value.Sha256())
              },
              AllowedScopes = { "mailApi" },
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
