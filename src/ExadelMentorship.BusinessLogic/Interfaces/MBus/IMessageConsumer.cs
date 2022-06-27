using System;

namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IMessageConsumer
    {
        void ReceiveMessage(Func<string, bool> callback, string queue, string key);
    }
}
