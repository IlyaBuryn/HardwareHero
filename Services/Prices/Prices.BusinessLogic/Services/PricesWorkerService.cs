using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Prices.BusinessLogic.Services
{
    public class PricesWorkerService : IPricesWorkerService
    {
        private readonly IMongoCollection<ContributorComponentPrices> _pricesCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseOptions _databaseSettings;

        public PricesWorkerService(
            IOptions<DatabaseOptions> databaseSettings,
            IMapper mapper)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _pricesCollection = mongoDb
                .GetCollection<ContributorComponentPrices>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.PricesCollection].CollectionName);

            _mapper = mapper;
        }

        public async Task<bool> DiscreetlyTryToUpdatePriceAsync(Guid contributorComponentPricesId)
        {
            throw new NotImplementedException();
        }
    }
}
