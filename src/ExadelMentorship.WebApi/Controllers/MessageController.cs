using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
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
        public string CreateOrder(string message)
        {
            _messagePublisher.SendMessage(message);

            return "Message Send";
        }
    }
}
