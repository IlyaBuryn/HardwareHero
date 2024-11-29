using Configurator.BusinessLogic.Models;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Data
{
    public class ConfiguratorDbContext : MongoDbContext
    {
        public ConfiguratorDbContext(IConfiguration configuration) 
            : base(configuration)
        {
            RegisterCollection<StoredAssembly>("Assemblies");
        }

        public IMongoCollection<StoredAssembly> MailEvents => GetCollection<StoredAssembly>();
    }
}