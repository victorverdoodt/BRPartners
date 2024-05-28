using BRPartners.Application.Commands;
using BRPartners.Application.Commands.Handlers;
using BRPartners.Infrastructure.Messaging.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BRPartners.Infrastructure.Messaging
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        private readonly EventingBasicConsumer _consumer;
        private readonly IConnectionFactoryCreator _connectionFactoryCreator;
        public RabbitMQConsumer(IMediator mediator, IServiceProvider serviceProvider, IConnectionFactoryCreator connectionFactoryCreator)
        {
            _mediator = mediator;
            _connectionFactoryCreator = connectionFactoryCreator;

            var factory = _connectionFactoryCreator.Get();

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _consumer = new(_channel);
            }
            catch (Exception ex)
            {

            }
            _serviceProvider = serviceProvider;
        }

        public async Task Run()
        {
            _channel.QueueDeclare(queue: "contacts", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _consumer.Received += async (object? sender, BasicDeliverEventArgs eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNameCaseInsensitive = true
                    };
                    var command = JsonSerializer.Deserialize<SyncMongoCommand>(message, options);
                    if (command != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                            await mediator.Send(command);
                        }
                    }
                    else
                    {
                        _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);

                    }
                    _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
                }
            };


            _channel.BasicConsume("contacts", false, _consumer);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Run();
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
