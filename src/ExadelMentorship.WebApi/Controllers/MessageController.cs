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
        public async Task<string> SendMail()
        {
            var message = new Message("sergi.lomtadze@gmail.com", "Test Mail", "This is mail from Exadel Mentorship");

            await _emailSender.SendEmailAsync(message);

            return "Mail Sent";
        }

        [HttpPost]
        public async Task<string> CreateOrder(string message)
        {
            await _messagePublisher.SendMessage(message,"fromWebApi");

            return "Message Sent";
        }
    }
}
