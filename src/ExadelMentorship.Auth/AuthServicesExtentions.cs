using ExadelMentorship.Auth.Interfaces;
using ExadelMentorship.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelMentorship.BusinessLogic
{
    public static class AuthServicesExtentions
    {
        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
