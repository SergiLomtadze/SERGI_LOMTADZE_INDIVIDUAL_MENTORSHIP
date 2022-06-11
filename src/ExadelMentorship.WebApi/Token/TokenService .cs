using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ExadelMentorship.WebApi.Token
{
    public class TokenService : ITokenService
    {
        private DiscoveryDocumentResponse _discDocument { get; set; }
        private AuthConfig _authConfig;
        public TokenService(IOptions<AuthConfig> authConfig)
        {
            _authConfig = authConfig.Value;
        }
        public async Task<string> GetToken(string userName,string password)
        {
            using (var client = new HttpClient())
            {
                _discDocument = await client.GetDiscoveryDocumentAsync(_authConfig.url);
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = _discDocument.TokenEndpoint,
                    ClientId = "swagger.client",
                    Password = password,
                    UserName = userName,
                    Scope = "WebApi.read",
                    ClientSecret = "secret"
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse.AccessToken;
            }
        }
    }
}
