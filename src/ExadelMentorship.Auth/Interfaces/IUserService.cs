using ExadelMentorship.Auth.Models;
namespace ExadelMentorship.Auth.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequest request);
    }
}
