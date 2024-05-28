namespace BRPartners.Domain.Interfaces
{
    public interface IRabbitMQPublisher
    {
        void Publish<T>(T message);
    }
}
