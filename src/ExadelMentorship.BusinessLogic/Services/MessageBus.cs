using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services
{
    public class MessageBus : IMessageProducer, IMessageConsumer
    {
        private RabbitMQSettings _rabbitMQSettings;
        public MessageBus(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public void ReceiveMessage(Func<string, bool> callback, string queue, string key)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind(queue, "direct-exchange", key);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                bool success = callback.Invoke(message);
                if (success)
                {
                    channel.BasicAck(e.DeliveryTag, true);
                }
            };

            channel.BasicConsume(queue, false, consumer);
        }

        public Task SendMessage<T>(T message, string key)
        {
            return Task.Run(() =>
            {
                var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);

                var messageToSend = new { Message = message };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageToSend));

                channel.BasicPublish("direct-exchange", key, null, body);
            });
        }
    }
}
