using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription
{
    public class UserUnSubscriptionCommandHandler : ICommandHandler<UserUnSubscriptionCommand, string>
    {
        private IReportUserRepo _reportUserRepo;
        public UserUnSubscriptionCommandHandler(IReportUserRepo reportUserRepo)
        {
            _reportUserRepo = reportUserRepo;
        }
        public async Task<string> Handle(UserUnSubscriptionCommand command)
        {
            await _reportUserRepo.SaveAsync(command.UserName, command.Email);
            return $"User: {command.UserName} succesfully subscribed";
        }
    }
}
