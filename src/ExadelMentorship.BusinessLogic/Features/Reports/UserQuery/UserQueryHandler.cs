using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserQuery
{
    public class UserQueryHandler : ICommandHandler<UserQuery, ReportUser[]>
    {
        private IReportUserRepo _reportUserRepo;
        public UserQueryHandler(IReportUserRepo reportUserRepo)
        {
            _reportUserRepo = reportUserRepo;
        }
        public Task<ReportUser[]> Handle(UserQuery command)
        {
            return _reportUserRepo.GelAll();
        }
    }
}
