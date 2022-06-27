using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services.Mail
{
    public class EmailSender : IEmailSender
    {
        private SMTPConfig _smtpConfig;
        public EmailSender(IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateMessage(message);
            var smtpClient = CreateClient();

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException e)
            {
                throw new SmtpException(e.Message);
            }
        }

        private MailMessage CreateMessage(Message message)
        {
            MailMessage mail = new MailMessage
            {
                Subject = message.Subject,
                Body = message.Content,
                From = new MailAddress(_smtpConfig.SenderAdress, _smtpConfig.SenderDssplayName),

            };

            mail.To.Add(message.To);
            mail.BodyEncoding = Encoding.Default;

            return mail;
        }
        private SmtpClient CreateClient()
        {
            return new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password)
            };
        }
    }
}
