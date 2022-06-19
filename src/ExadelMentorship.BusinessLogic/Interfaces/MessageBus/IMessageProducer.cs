using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IMessageProducer
    {
        Task SendMessage<T>(T message);
    }
}
