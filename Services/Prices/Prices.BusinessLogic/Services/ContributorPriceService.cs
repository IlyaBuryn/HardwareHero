using HardwareHero.Shared.Models;
using HardwareHero.Shared.Requests;
using HardwareHero.Shared.Responses;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Prices.BusinessLogic.Services
{
    public class ContributorPriceService : IContributorPricesService
    {
        private readonly IMongoCollection<ContributorComponentPrices> _pricesCollection;
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
                .GetCollection<ContributorComponentPrices>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.PricesCollection].CollectionName);

            _mapper = mapper;
        }

        public async Task<Guid?> ChangePriceAsync(ChangePriceRequest request)
        {
            var prices = await _pricesCollection.Find(
                x => x.ComponentId == request.ComponentId &&
                x.ContributorId == request.ContributorId)
                .ToListAsync();

            var price = prices.FirstOrDefault();

            if (price == null)
            {
                var newPrice = new ContributorComponentPrices()
                {
                    Id = Guid.NewGuid(),
                    ComponentId = request.ComponentId,
                    ContributorId = request.ContributorId,
                    Prices = new List<PriceStamp>()
                    {
                        new()
                        {
                            Price = request.NewPrice,
                            Timestamp = DateTime.Now,
                        }
                    }
                };

                await _pricesCollection.InsertOneAsync(newPrice);

                return newPrice.Id;
            }

            price.Prices.Add(new PriceStamp()
            {
                Price = request.NewPrice,
                Timestamp = DateTime.Now,
            });

            var filter = Builders<ContributorComponentPrices>.Filter.Eq("_id", price.Id);
            var update = Builders<ContributorComponentPrices>.Update
                .Set("Prices", price.Prices);

            var result = await _pricesCollection.ReplaceOneAsync(filter, price);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return price.Id;
            }
            else
            {
                throw new Exception("Can't update element!");
            }
        }

        public async Task<List<ContributorComponentPricesDto?>> GetComponentPricesAsync(Guid componentId)
        {
            var prices = await _pricesCollection.Find(x => x.ComponentId == componentId).ToListAsync();
            if (prices == null || prices.Count == 0)
            {
                throw new NotFoundException(nameof(prices));
            }

            var result = _mapper.Map<List<ContributorComponentPricesDto?>>(prices);

            return result;
        }

        //public async Task<PageResponse<ContributorComponentPricesDto?>> GetPricesToDiscreetlyUpdate(PaginationInfo pageInfo)
        //{
        //    var filter = Builders<ContributorComponentPrices>.Filter.Empty;
        //    long totalCount = await _pricesCollection.CountDocumentsAsync(filter);
        //    int totalPages = (int)Math.Ceiling((double)totalCount / pageInfo.PageSize);
        //    int skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;

        //    var options = new FindOptions<ContributorComponentPrices>
        //    {
        //        Limit = pageInfo.PageSize,
        //        Skip = skip
        //    };

        //    var prices = await _pricesCollection.FindAsync(filter, options);
        //    var pricesList = _mapper.Map<List<ContributorComponentPricesDto?>>(prices.ToList());
        //    var result = new PageResponse<ContributorComponentPricesDto>()
        //    {
        //        CurrentPaginationInfo = pageInfo,
        //        Items = pricesList,
        //        TotalPages = totalPages
        //    };

        //    return result;
        //}
    }
}
