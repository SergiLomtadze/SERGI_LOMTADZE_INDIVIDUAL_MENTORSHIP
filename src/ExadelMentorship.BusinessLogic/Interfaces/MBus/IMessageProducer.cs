using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message, string key);
    }
}
