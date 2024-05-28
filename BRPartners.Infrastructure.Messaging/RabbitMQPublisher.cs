using BRPartners.Domain.Interfaces;
using BRPartners.Infrastructure.Messaging.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BRPartners.Infrastructure.Messaging
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConnectionFactoryCreator _connectionFactoryCreator;

        public RabbitMQPublisher(IConnectionFactoryCreator connectionFactoryCreator)
        {
            _connectionFactoryCreator = connectionFactoryCreator;

            var factory = _connectionFactoryCreator.Get();

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publish<T>(T message)
        {
            //teste
            _channel.QueueDeclare(queue: "contacts",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                                 routingKey: "contacts",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
