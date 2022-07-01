using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Services.Mail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExadelMentorship.BackgroundApp
{
    public class MainProcess : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IEmailSender _emailSender;
        public MainProcess(IMessageConsumer messageConsumer,
            IEmailSender emailSender)
        {
            _messageConsumer = messageConsumer;
            _emailSender = emailSender;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.ReceiveMessage(ProcessMail, "mailQueue", "sendMail");            
            while (!stoppingToken.IsCancellationRequested)
            {
            };
            return Task.CompletedTask;
        }
        private async Task<bool> ProcessMail(string message)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(message);
            JObject messageObj = obj["Message"] as JObject;
            var mail = (string)messageObj["Email"];            
            var user = (string)messageObj["UserName"];
            JObject reportObj = messageObj["Report"] as JObject;
            var report = (string)reportObj["Result"];

            var messageToSend = new Message(mail, $"Report For {user}", report);
            try
            {
                await _emailSender.SendEmailAsync(messageToSend);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}