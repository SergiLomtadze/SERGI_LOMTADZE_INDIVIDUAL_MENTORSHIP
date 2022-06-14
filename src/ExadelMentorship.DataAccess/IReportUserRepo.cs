﻿using ExadelMentorship.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IReportUserRepo
    {
        IQueryable<ReportUser> GelAll();
        Task SaveAsync(string userName, string email);

        void Delete(int userId);
    }
}
