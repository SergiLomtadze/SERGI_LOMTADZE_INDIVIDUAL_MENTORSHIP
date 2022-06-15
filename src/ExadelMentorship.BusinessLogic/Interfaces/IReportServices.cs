using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface IReportServices
    {
        Task<string> GenerateReportForUser(int userId);
    }
}
