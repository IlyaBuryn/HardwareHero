using MongoDB.Driver;
using System.Collections.Concurrent;

namespace HardwareHero.Shared.Repositories.Mongo
{
    public abstract class MongoDbContext
    {
        private IMongoDatabase _database;
        private IMongoClient _client;

        private readonly ConcurrentDictionary<Type, object> _collections = new();

        internal void Initialize(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        protected IMongoCollection<T> Collection<T>(string collectionName)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("MongoDbContext is not initialized. Call AddMongoDbContext first.");
            }

            return (IMongoCollection<T>)_collections.GetOrAdd(typeof(T), _ =>
                _database.GetCollection<T>(collectionName));
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            if (_database == null)
            {
                throw new InvalidOperationException("MongoDbContext is not initialized. Call AddMongoDbContext first.");
            }

            var collectionName = typeof(T).Name;
            return Collection<T>(collectionName);
        }
    }
}