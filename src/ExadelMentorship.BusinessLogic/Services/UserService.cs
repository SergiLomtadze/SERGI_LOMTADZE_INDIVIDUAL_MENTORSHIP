using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        Task<AuthenticationModel> IUserService.GetTokenAsync(TokenRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
