using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ExadelMentorship.WebApi.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        private RabbitMQSettings _rabbitMQSettings;
        public RabbitMQProducer(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }
        public void SendMessage<T>(T text)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);

            var message = new { Name = "Producer", Message = text };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("direct-exchange", "firstTest", null, body);
        }
    }
}
