using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BRPartners.Infrastructure.Data.Persistence.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("ContactDb");
        }

        public IMongoDatabase Database => _database;
    }
}
