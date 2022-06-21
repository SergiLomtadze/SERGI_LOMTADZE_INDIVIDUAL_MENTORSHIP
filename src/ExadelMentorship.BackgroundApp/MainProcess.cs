using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;

namespace ExadelMentorship.BackgroundApp
{
    public class MainProcess : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;

        public MainProcess(IMessageConsumer messageConsumer)
        {
            _messageConsumer = messageConsumer;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messageConsumer.ReceiveMessage(ProcessMessage);
            }
            return Task.CompletedTask;
        }
        private void ProcessMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}