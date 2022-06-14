using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription
{
    public class UserSubscriptionCommand : ICommand<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
