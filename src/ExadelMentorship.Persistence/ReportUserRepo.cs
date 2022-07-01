using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelMentorship.Persistence
{
    public class ReportUserRepo : IReportUserRepo
    {
        private IServiceProvider _services;
        public ReportUserRepo(IServiceProvider services)
        {
            _services = services;
        }

        public Task<IEnumerable<ReportUser>> GetAll()
        {
            var scope = _services.CreateScope();
            return Task.FromResult(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().ReportUsers.AsEnumerable());
        }

        public async Task SaveAsync(string userName, string email, string cities, int period)
        {
            var scope = _services.CreateScope();
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().ReportUsers.Add(new ReportUser
            {
                UserName = userName,
                Email = email,
                Cities = cities,
                Period = period
            });

            await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().SaveChangesAsync();
        }

        public void Delete(int userId)
        {
            var scope = _services.CreateScope();
            var user = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().ReportUsers.Find(userId);
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Remove(user);
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().SaveChanges();
        }

        public Task<ReportUser> GetById(int id)
        {
            var scope = _services.CreateScope();
            return Task.FromResult(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                .ReportUsers.Where(x => x.Id==id).FirstOrDefault());
        }
    }
}
