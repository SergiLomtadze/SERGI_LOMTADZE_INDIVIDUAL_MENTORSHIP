using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.Reports.UserQuery;
using ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription;
using ExadelMentorship.BusinessLogic.Features.Reports.UserUnSubscription;
using ExadelMentorship.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly CommandInvoker _commandInvoker;
        private readonly ILogger<WeatherController> _logger;
        public ReportController(CommandInvoker commandInvoker, ILogger<WeatherController> logger)
        {
            _commandInvoker = commandInvoker;
            _logger = logger;
        }

        [HttpPost]
        public Task<string> SubscribeUser([FromBody] UserSubscriptionCommand input)
        {
            _logger.LogInformation($"Subscribed user name: {input.UserName}");
            return _commandInvoker.Invoke
            (
                new UserSubscriptionCommand
                {
                    UserName = input.UserName,
                    Email = input.Email
                }
            );
        }

        [HttpDelete("UnsubscribeUser/{userId}")]
        public Task<string> UnsubscribeUser([FromRoute] int userId)
        {
            _logger.LogInformation($"Unsubscribed user id: {userId}");
            return _commandInvoker.Invoke
            (
                new UnSubscribeUserCommand
                {
                    userId = userId
                }
            );
        }

        [HttpGet]
        public Task<IEnumerable<ReportUser>> GetAllSubscribedUser()
        {
            return _commandInvoker.Invoke
            (
                new UserQuery
                {
                }
            );
        }
    }
}
