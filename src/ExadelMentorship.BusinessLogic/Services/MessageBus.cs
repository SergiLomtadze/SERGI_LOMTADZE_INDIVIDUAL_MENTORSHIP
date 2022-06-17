using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Services
{
    public class MessageBus : IMessageProducer, IMessageConsumer
    {
        private RabbitMQSettings _rabbitMQSettings;
        public MessageBus(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public void ReceiveMessage(Action<string> callback)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare("direct-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind("direct-queue", "direct-exchange", "firstTest");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                callback.Invoke(message);
            };

            channel.BasicConsume("direct-queue",false,consumer);
        }

        public void SendMessage<T>(T message)
        {

            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);

            var messageToSend = new { Name = "WebApi", Message = message };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageToSend));

            channel.BasicPublish("direct-exchange", "firstTest", null, body);
        }
    }
}
