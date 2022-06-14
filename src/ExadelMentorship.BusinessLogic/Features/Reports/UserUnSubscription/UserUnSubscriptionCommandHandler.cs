﻿using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserUnSubscription
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
            _reportUserRepo.Delete(command.userId);
            return await Task.FromResult($"User with Id: {command.userId} succesfully unsubscribed");
        }
    }
}
