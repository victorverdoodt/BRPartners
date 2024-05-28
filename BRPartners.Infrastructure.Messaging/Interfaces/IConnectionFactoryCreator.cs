using RabbitMQ.Client;

namespace BRPartners.Infrastructure.Messaging.Interfaces
{
    public interface IConnectionFactoryCreator
    {
        ConnectionFactory Get();
    }
}
