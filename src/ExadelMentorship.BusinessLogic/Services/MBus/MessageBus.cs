using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services.MBus
{
    public class MessageBus : IMessageProducer, IMessageConsumer
    {
        private readonly IConnectionProvider _connectionProvider;
        public MessageBus(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void ReceiveMessage(Func<string, bool> callback, string queue, string key)
        {
            var channel = _connectionProvider.GetConnection().CreateModel();
            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind(queue, "direct-exchange", key);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
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
                var channel = _connectionProvider.GetConnection().CreateModel();
                channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);

                var messageToSend = new { Message = message };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageToSend));

                channel.BasicPublish("direct-exchange", key, null, body);
            });
        }
    }
}
