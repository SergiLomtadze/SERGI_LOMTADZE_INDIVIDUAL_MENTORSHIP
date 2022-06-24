using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Services.MBus
{
    public class ConnectionProvider : IConnectionProvider, IDisposable
    {
        private RabbitMQSettings _rabbitMQSettings;
        private IConnection _connection;

        public ConnectionProvider(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public IConnection GetConnection()
        {
            _connection = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            }.CreateConnection();

            return _connection;
        }

    }
}
