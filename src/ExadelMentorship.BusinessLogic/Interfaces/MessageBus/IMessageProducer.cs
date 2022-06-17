namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
