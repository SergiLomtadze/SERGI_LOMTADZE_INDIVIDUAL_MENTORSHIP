using IdentityModel.Client;

namespace ExadelMentorship.WebApi.Token
{
    public class TokenService : ITokenService
    {
        private DiscoveryDocumentResponse _discDocument { get; set; }
        public TokenService()
        {
            using (var client = new HttpClient())
            {
                _discDocument = client.GetDiscoveryDocumentAsync("https://localhost:7162/.well-known/openid-configuration").Result;
            }
        }
        public async Task<string> GetToken(string userName,string password)
        {
            using (var client = new HttpClient())
            {
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
