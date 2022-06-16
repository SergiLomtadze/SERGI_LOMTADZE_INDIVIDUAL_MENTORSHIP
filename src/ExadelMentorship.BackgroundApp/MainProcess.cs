using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ExadelMentorship.BackgroundApp
{
    public class MainProcess : BackgroundService
    {
        private RabbitMQSettings _rabbitMQSettings;
        public MainProcess(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_rabbitMQSettings.Uri)
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                DirectExchangeConsumer.Consume(channel);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}