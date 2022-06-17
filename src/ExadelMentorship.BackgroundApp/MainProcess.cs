using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ExadelMentorship.BackgroundApp
{
    public class MainProcess : BackgroundService
    {
        private readonly IMessageConsumer _messagePublisher;

        public MainProcess(IMessageConsumer messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messagePublisher.ReceiveMessage();

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}