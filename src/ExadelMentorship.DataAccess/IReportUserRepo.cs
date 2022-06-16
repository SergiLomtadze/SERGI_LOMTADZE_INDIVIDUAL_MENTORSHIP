using ExadelMentorship.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IReportUserRepo
    {
        Task<ReportUser> GetById(int id);
        Task<IEnumerable<ReportUser>> GetAll();
        Task SaveAsync(string userName, string email, string cities, int period);
        void Delete(int userId);
    }
}
