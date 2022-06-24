using RabbitMQ.Client;

namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IConnectionProvider
    {
        IConnection GetConnection();
    }
}
