using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces.MessageBus
{
    public interface IMessageConsumer
    {
        void ReceiveMessage(Func<string, Task<bool>> callback, string queue, string key);
    }
}
