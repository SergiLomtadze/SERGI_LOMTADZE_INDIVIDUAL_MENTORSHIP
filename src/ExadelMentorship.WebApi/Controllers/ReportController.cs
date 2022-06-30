using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.Reports.UserQuery;
using ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription;
using ExadelMentorship.BusinessLogic.Features.Reports.UserUnSubscription;
using ExadelMentorship.BusinessLogic.Interfaces;
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
        private readonly IReportServices _reportServices;
        public ReportController(CommandInvoker commandInvoker, 
            ILogger<WeatherController> logger,
            IReportServices reportServices)
        {
            _commandInvoker = commandInvoker;
            _logger = logger;
            _reportServices = reportServices;
        }

        [HttpGet("ReportByUserId/{userId}")]
        public Task<string> GetReportByUserId([FromRoute] int userId)
        {
            return _reportServices.GenerateReportForUser(userId);
        }

        [HttpPost("SubscribeUser")]
        public Task<string> SubscribeUser([FromBody] UserSubscriptionCommand input)
        {
            _logger.LogInformation($"Subscribed user name: {input.UserName}");
            return _commandInvoker.Invoke
            (
                input
            );
        }

        [HttpDelete("UnsubscribeUser/{userId}")]
        public Task<string> UnsubscribeUser([FromRoute] UnSubscribeUserCommand userId)
        {
            _logger.LogInformation($"Unsubscribed user id: {userId}");
            return _commandInvoker.Invoke
            (
                userId
            );
        }

        [HttpGet("AllSubscribedUser")]
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
