using ExadelMentorship.BusinessLogic.Models;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequest request);
    }
}
