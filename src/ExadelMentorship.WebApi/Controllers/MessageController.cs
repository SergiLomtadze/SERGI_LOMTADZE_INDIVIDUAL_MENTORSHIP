using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageProducer _messagePublisher;
        private readonly IEmailSender _emailSender;
        private MailServerSettings _mailServerSettings;

        public MessageController(IMessageProducer messagePublisher, IEmailSender emailSender, IOptions<MailServerSettings> mailServerSettings)
        {
            _messagePublisher = messagePublisher;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings.Value;
        }

        [HttpGet("TestMail")]
        public async Task SendMail()
        {
            var message = new Message("sergi.lomtadze@gmail.com", "Test Mail", "This is mail from Exadel Mentorship");
            await _emailSender.SendEmailAsync(message);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPost("SendMail")]
        public async Task PublishMessage([FromBody] Message message)
        {
            if (_mailServerSettings.Local)
            {
                await _emailSender.SendEmailAsync(message);
            }
            else
            {
                await _messagePublisher.SendMessage(message, "sendMail");
            }        
        }
    }
}
