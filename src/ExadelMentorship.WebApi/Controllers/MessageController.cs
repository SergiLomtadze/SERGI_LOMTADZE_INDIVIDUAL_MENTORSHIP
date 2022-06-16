using ExadelMentorship.WebApi.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageProducer _messagePublisher;

        public MessageController(IMessageProducer messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string message)
        {
            _messagePublisher.SendMessage(message);

            return await Task.FromResult(Ok("Message Send"));
        }
    }
}
