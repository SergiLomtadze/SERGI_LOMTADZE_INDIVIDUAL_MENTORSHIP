using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.Persistence
{
    public class ReportUserRepo : IReportUserRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportUserRepo(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<ReportUser> GelAll()
        {
            return _dbContext.ReportUsers;
        }

        public async Task SaveAsync(string userName, string email)
        {
            _dbContext.Add(new ReportUser
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
        }
    }
}
