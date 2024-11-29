using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Prices.BusinessLogic.Models;

namespace Prices.BusinessLogic.Data
{
    public class PricesDbContext : MongoDbContext
    {
        public PricesDbContext(IConfiguration configuration) 
            : base(configuration)
        {
            RegisterCollection<ContributorComponentPrices>("ContributorPrices");
        }

        public IMongoCollection<ContributorComponentPrices> Prices 
            => GetCollection<ContributorComponentPrices>();
    }
}
