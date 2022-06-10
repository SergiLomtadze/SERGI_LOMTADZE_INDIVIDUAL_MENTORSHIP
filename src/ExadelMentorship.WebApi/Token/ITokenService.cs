using IdentityModel.Client;

namespace ExadelMentorship.WebApi.Token
{
    public interface ITokenService
    {
        Task<string> GetToken(string password, string userName);
    }
}
