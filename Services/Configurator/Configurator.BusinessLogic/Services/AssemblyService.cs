using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class AssemblyService : IAssemblyService
    {
        private readonly IMongoCollection<StoredAssembly> _assembliesCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseOptions _databaseSettings;

        public AssemblyService(
            IOptions<DatabaseOptions> databaseSettings,
            IMapper mapper)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _assembliesCollection = mongoDb
                .GetCollection<StoredAssembly>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.AssembliesCollection].CollectionName);

            _mapper = mapper;
        }

        public async Task<List<StoredAssemblyDto?>> GetAssembliesByUserIdAsync(Guid userId)
        {
            var filter = Builders<StoredAssembly>.Filter.Eq(a => a.UserId, userId);

            var assemblies = await _assembliesCollection.Find(filter).ToListAsync();
            if (assemblies == null || assemblies.Count == 0)
            {
                throw new NotFoundException(nameof(assemblies));
            }

            var result = _mapper.Map<List<StoredAssemblyDto?>>(assemblies);

            return result;
        }

        public async Task<Guid?> SaveAssemblyAsync(StoredAssemblyDto assemblyToAdd)
        {
            assemblyToAdd.Id = Guid.NewGuid();
            assemblyToAdd.Timestamp = DateTime.Now;

            var assembly = _mapper.Map<StoredAssembly>(assemblyToAdd);
            await _assembliesCollection.InsertOneAsync(assembly);

            return assemblyToAdd.Id;
        }

        public async Task<bool> UpdateAssemblyAsync(StoredAssemblyDto assemblyToUpdate)
        {
            var filter = Builders<StoredAssembly>.Filter.Eq(a => a.Id, assemblyToUpdate.Id);

            var assembly = await _assembliesCollection.Find(filter).FirstOrDefaultAsync();
            if (assembly == null)
            {
                throw new NotFoundException(nameof(assembly));
            }

            assembly.SelectedComponents = _mapper.Map<List<ConfiguratorComponent>>(assemblyToUpdate.SelectedComponents);
            var updateResult = await _assembliesCollection.ReplaceOneAsync(filter, assembly);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> RemoveAssemblyAsync(Guid assemblyId)
        {
            var filter = Builders<StoredAssembly>.Filter.Eq(a => a.Id, assemblyId);

            var assembly = await _assembliesCollection.Find(filter).FirstOrDefaultAsync();
            if (assembly == null)
            {
                throw new NotFoundException(nameof(assembly));
            }

            var deleteResult = await _assembliesCollection.DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }
    }
}
