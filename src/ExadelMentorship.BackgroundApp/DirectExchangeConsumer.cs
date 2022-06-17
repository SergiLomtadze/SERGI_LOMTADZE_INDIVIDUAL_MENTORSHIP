using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ExadelMentorship.BackgroundApp
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
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
                Console.WriteLine(message);
            };

            channel.BasicConsume("direct-queue", true, consumer);

            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
