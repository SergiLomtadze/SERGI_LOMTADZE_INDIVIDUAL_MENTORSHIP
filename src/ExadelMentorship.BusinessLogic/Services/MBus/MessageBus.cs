using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
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
        private readonly IConnection _connection;
        public MessageBus(IConnection connection)
        {
            _connection = connection;
        }

        public void ReceiveMessage(Func<string, bool> callback, string queue, string key)
        {
            var channel = _connection.CreateModel();
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
            //Not to make blocking I/O operation
            return Task.Run(() =>
            {
                var channel = _connection.CreateModel();
                channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);

                var messageToSend = new { Message = message };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageToSend));

                channel.BasicPublish("direct-exchange", key, null, body);
            });
        }
    }
}
