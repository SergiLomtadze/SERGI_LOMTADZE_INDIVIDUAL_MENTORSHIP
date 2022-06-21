using ExadelMentorship.BusinessLogic.Services.Mail;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
