using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using BRPartners.Infrastructure.Data.Persistence.Context;
using BRPartners.Infrastructure.Data.Persistence.Context.Repositories;
using BRPartners.Infrastructure.Data.Persistence.Mongo;
using BRPartners.Infrastructure.Data.Persistence.Mongo.Repositories;
using BRPartners.Infrastructure.Messaging;
using BRPartners.Infrastructure.Messaging.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BRPartners.Infrastructure.DI
{
    public class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionFactoryCreator, ConnectionFactoryCreator>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));

            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddSingleton<MongoDbContext>();

            services.AddScoped<IMongoDatabase>(provider =>
            {
                var mongoContext = provider.GetRequiredService<MongoDbContext>();
                return mongoContext.Database;
            });

            // Repositório para o MongoDB
            services.AddScoped<IRepository<Contact>, MongoRepository<Contact>>();

            // Configuração do RabbitMQ Publisher
            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();


        }
    }
}
