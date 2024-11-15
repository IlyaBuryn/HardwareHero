using Configurator.BusinessLogic.Models;
using HardwareHero.Shared.Repositories.Mongo;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Data
{
    public class ConfiguratorDbContext : MongoDbContext
    {
        public IMongoCollection<StoredAssembly> Assemblies => Collection<StoredAssembly>("Assemblies");
    }
}