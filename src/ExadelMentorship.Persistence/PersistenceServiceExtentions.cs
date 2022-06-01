using ExadelMentorship.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelMentorship.Persistence
{
    public static class PersistenceServiceExtentions
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((s, option) =>
                option.UseSqlServer(s.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")));
        }
    }
}
