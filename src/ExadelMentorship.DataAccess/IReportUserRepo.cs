using ExadelMentorship.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IReportUserRepo
    {
        IQueryable<ReportUser> GelAll();
        Task SaveAsync(string userName, string email);
    }
}
