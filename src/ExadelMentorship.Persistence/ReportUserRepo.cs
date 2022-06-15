using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;

namespace ExadelMentorship.Persistence
{
    public class ReportUserRepo : IReportUserRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportUserRepo(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public Task<IEnumerable<ReportUser>> GelAll()
        {
            return Task.FromResult(_dbContext.ReportUsers.AsEnumerable());
        }

        public async Task SaveAsync(string userName, string email)
        {
            _dbContext.ReportUsers.Add(new ReportUser
            {
                UserName = userName,
                Email = email,

            });
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(int userId)
        {
            var user = _dbContext.ReportUsers.Find(userId);
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
