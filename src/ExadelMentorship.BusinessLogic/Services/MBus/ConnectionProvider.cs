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
        private bool _disposed;

        public ConnectionProvider(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public IConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new ConnectionFactory
                {
                    Uri = new Uri(_rabbitMQSettings.Uri)
                }.CreateConnection();
            }
            return _connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Close();
                }

                _disposed = true;
            }
        }

    }
}
