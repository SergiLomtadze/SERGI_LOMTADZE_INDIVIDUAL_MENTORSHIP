using ExadelMentorship.Auth.Interfaces;
using ExadelMentorship.Auth.Models;

namespace ExadelMentorship.Auth.Services
{
    public class UserService : IUserService
    {
        Task<AuthenticationModel> IUserService.GetTokenAsync(TokenRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
