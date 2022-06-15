using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserQuery
{
    public class UserQuery : ICommand<IEnumerable<ReportUser>>
    {
    }
}
