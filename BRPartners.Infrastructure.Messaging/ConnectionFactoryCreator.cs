using BRPartners.Infrastructure.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace BRPartners.Infrastructure.Messaging
{
    public class ConnectionFactoryCreator : IConnectionFactoryCreator
    {
        private readonly IConfiguration _configuration;
        public ConnectionFactoryCreator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ConnectionFactory Get()
        {
            var factory = new ConnectionFactory() { HostName = _configuration["Rabbitmq:HostName"], UserName = _configuration["Rabbitmq:UserName"], Password =  _configuration["Rabbitmq:Password"], VirtualHost = "/" };
            return factory;
        }
    }
}
