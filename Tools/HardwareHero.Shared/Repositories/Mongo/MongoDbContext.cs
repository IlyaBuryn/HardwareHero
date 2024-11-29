using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Concurrent;

namespace HardwareHero.Shared.Repositories.Mongo
{
    public abstract class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly ConcurrentDictionary<Type, object> _collections = new();

        protected MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDbSettings:ConnectionString"];
            var databaseName = configuration["MongoDbSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        protected void RegisterCollection<TEntity>(string collectionName)
        {
            _collections[typeof(TEntity)] = _database.GetCollection<TEntity>(collectionName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            if (_collections.TryGetValue(typeof(TEntity), out var collection))
            {
                return (IMongoCollection<TEntity>)collection;
            }

            throw new InvalidOperationException($"The collection: {typeof(TEntity).Name} is not registered!");
        }
    }
}