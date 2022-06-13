using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription
{
    public class UserSubscriptionCommandHandler : ICommandHandler<UserSubscriptionCommand, string>
    {
        private IReportUserRepo _reportUserRepo;
        public UserSubscriptionCommandHandler(IReportUserRepo reportUserRepo)
        {
            _reportUserRepo = reportUserRepo;
        }
        public async Task<string> Handle(UserSubscriptionCommand command)
        {
            await _reportUserRepo.SaveAsync(command.UserName, command.Email);
            return $"User: {command.UserName} succesfully added";
        }
    }
}
