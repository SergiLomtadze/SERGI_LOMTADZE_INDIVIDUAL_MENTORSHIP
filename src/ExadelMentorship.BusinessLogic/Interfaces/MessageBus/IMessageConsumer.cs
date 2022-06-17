using System;

namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IMessageConsumer
    {
        void ReceiveMessage(Action<string> callback);
    }
}
