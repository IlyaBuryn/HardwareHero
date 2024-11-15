using HardwareHero.Shared.Repositories.Mongo;
using MongoDB.Driver;
using Prices.BusinessLogic.Models;

namespace Prices.BusinessLogic.Data
{
    public class PricesDbContext : MongoDbContext
    {
        public IMongoCollection<ContributorComponentPrices> Prices 
            => Collection<ContributorComponentPrices>("ContributorPrices");
    }
}
