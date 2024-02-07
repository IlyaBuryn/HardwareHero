using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Prices.BusinessLogic.Services
{
    public class ContributorPriceService : IContributorPricesService
    {
        private readonly IMongoCollection<ContributorPrice> _pricesCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseOptions _databaseSettings;

        public ContributorPriceService(
            IOptions<DatabaseOptions> databaseSettings,
            IMapper mapper)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _pricesCollection = mongoDb
                .GetCollection<ContributorPrice>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.PricesCollection].CollectionName);

            _mapper = mapper;
        }

        public async Task<List<ContributorPriceDto?>> GetContributorPricesByComponentIdAsync(Guid componentId)
        {
            var prices = await _pricesCollection.Find(x => x.ComponentId == componentId).ToListAsync();
            if (prices == null || prices.Count == 0)
            {
                throw new NotFoundException(nameof(prices));
            }

            var result = _mapper.Map<List<ContributorPriceDto?>>(prices);

            return result;
        }

        public async Task<ContributorPriceDto?> GetLastPriceAsync(Guid componentId)
        {
            var prices = await _pricesCollection.Find(x => x.ComponentId == componentId).ToListAsync();
            if (prices == null || prices.Count == 0)
            {
                throw new NotFoundException(nameof(prices));
            }

            var latestPrice = prices.OrderBy(x => Math
                .Abs((x.Timestamp - DateTime.Now).Ticks)).FirstOrDefault();
            var result = _mapper.Map<ContributorPriceDto?>(latestPrice);

            return result;
        }

        public async Task<Guid?> AddContributorPriceAsync(ContributorPriceDto priceToAdd)
        {
            if (priceToAdd.Id == Guid.Empty)
            {
                priceToAdd.Id = Guid.NewGuid();
            }

            priceToAdd.Timestamp = DateTime.Now;
            var assembly = _mapper.Map<ContributorPrice>(priceToAdd);
            await _pricesCollection.InsertOneAsync(assembly);

            return priceToAdd.Id;
        }
    }
}
