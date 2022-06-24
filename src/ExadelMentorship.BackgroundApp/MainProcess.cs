using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

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
            _messageConsumer.ReceiveMessage(ProcessMessage,"webApiQueue", "fromWebApi");
            while (!stoppingToken.IsCancellationRequested)
            {
            };

            return Task.CompletedTask;
        }
        private bool ProcessMessage(string message)
        {
            Console.WriteLine(message);
            return true;
        }
    }
}