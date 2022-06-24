using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Services.MBus
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly ConnectionFactory _factory;

        private readonly IConnection _connection;

        public ConnectionProvider(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMQSettings.Value.Uri)
            };
            _connection = _factory.CreateConnection();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

    }
}
