using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageProducer _messagePublisher;
        private readonly IEmailSender _emailSender;

        public MessageController(IMessageProducer messagePublisher, IEmailSender emailSender)
        {
            _messagePublisher = messagePublisher;
            _emailSender = emailSender;
        }

        [HttpGet("TestMail")]
        public string SendMail()
        {
            var message = new Message("sergi.lomtadze@gmail.com", "TestMail", "This is mail from .net app");
            
            _emailSender.SendEmail(message);

            return "Mail Sent";
        }

        [HttpPost]
        public string CreateOrder(string message)
        {
            _messagePublisher.SendMessage(message);

            return "Message Send";
        }
    }
}
