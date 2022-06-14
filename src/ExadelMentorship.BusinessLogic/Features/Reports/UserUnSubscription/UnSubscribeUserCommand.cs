using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserUnSubscription
{
    public class UnSubscribeUserCommand : ICommand<string>
    {
        public int userId { get; set; }
    }
}
