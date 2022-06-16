using ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Validators
{
    public class UserSubscriptionValidator : AbstractValidator<UserSubscriptionCommand>
    {
        public UserSubscriptionValidator()
        {
            RuleFor(x => x.Period).Must(x =>
                x.Equals(1) || x.Equals(3) || x.Equals(12) || x.Equals(24));
        }
    }
}
