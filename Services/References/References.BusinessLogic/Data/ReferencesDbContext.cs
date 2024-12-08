using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using References.BusinessLogic.Models;

namespace References.BusinessLogic.Data
{
    public class ReferencesDbContext : MongoDbContext
    {
        public ReferencesDbContext(IConfiguration configuration)
            : base(configuration)
        {
            RegisterCollection<Currency>("Currencies");
            RegisterCollection<Region>("Regions");
        }

        public IMongoCollection<Currency> Currencies
            => GetCollection<Currency>();

        public IMongoCollection<Region> Regions
            => GetCollection<Region>();
    }
}
