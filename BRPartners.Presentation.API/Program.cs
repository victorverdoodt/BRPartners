using BRPartners.Application.Commands.Handlers;
using BRPartners.Application.Queries.Handlers;
using BRPartners.Infrastructure.DI;
using BRPartners.Infrastructure.Messaging;

namespace BRPartners.Presentation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            RegisterServices(builder.Services, builder.Configuration);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(CreateContactCommandHandler).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(SyncMongoCommandHandler).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(GetContactsQueryHandler).Assembly);
            });
            builder.Services.AddHostedService<RabbitMQConsumer>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjection.RegisterServices(services, configuration);
        }
    }
}
