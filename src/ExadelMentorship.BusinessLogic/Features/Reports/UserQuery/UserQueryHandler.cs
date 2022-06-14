using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserQuery
{
    public class UserQueryHandler : ICommandHandler<UserQuery, IEnumerable<ReportUser>>
    {
        private IReportUserRepo _reportUserRepo;
        public UserQueryHandler(IReportUserRepo reportUserRepo)
        {
            _reportUserRepo = reportUserRepo;
        }
        public async Task<IEnumerable<ReportUser>> Handle(UserQuery command)
        {
            var response = new List<ReportUser>();
            var list = _reportUserRepo.GelAll();

            if (list.Any())
            {
                foreach (var item in list)
                {
                    response.Add(new ReportUser
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        Email = item.Email
                    });
                }
            }

            return await Task.FromResult(response);
        }
    }
}
