using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Configurator.BusinessLogic.Services
{
    public class DataService : IDataService
    {
        private readonly DatabaseOptions _databaseSettings;
        private readonly ILogger<DataService> _logger;
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDb;

        public DataService(IOptions<DatabaseOptions> databaseSettings, ILogger<DataService> logger)
        {
            _databaseSettings = databaseSettings.Value;
            _mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            _mongoDb = _mongoClient.GetDatabase(_databaseSettings.DatabaseName);
            _logger = logger;
        }

        // TODO: Need new solution
        public async Task EnsureDatabaseFromFileAsync<T>(string filePath, string collectionName) where T : class
        {
            //IMongoCollection<T> _collection = _mongoDb.GetCollection<T>(
            //    _databaseSettings.Collections[collectionName].CollectionName);

            //string jsonData = File.ReadAllText(filePath);
            //var components = JsonConvert.DeserializeObject<List<T>>(jsonData);

            //ConfigureOptions();

            //try
            //{
            //    await _collection.InsertManyAsync(components);
            //}
            //catch
            //{
            //    _logger.LogInformation("Configurator: Config data is already exist!");
            //}
        }

        private void ConfigureOptions()
        {
            var pack = new ConventionPack
            {
                new IgnoreIfNullConvention(true)
            };
            ConventionRegistry.Register("IgnoreIfNull", pack, t => true);
        }
    }
}
