using AutoMapper;
using Configurator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Configurator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Configurator;
using HardwareHero.Services.Shared.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class AssemblyService : IAssemblyService
    {
        private readonly IMongoCollection<CustomAssembly> _assembliesCollection;
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
                .GetCollection<CustomAssembly>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.AssembliesCollection].CollectionName);

            _mapper = mapper;
        }

        public async Task<Guid?> AddAssemblyAsync(CustomAssemblyDto assemblyToAdd)
        {
            assemblyToAdd.Id = Guid.NewGuid();
            assemblyToAdd.CreationDate = DateTime.Now;
            if (assemblyToAdd.AssemblyCategory == null)
            {
                assemblyToAdd.AssemblyCategory = "PC";
            }

            var assembly = _mapper.Map<CustomAssembly>(assemblyToAdd);
            await _assembliesCollection.InsertOneAsync(assembly);

            return assemblyToAdd.Id;
        }

        public async Task<List<CustomAssemblyDto?>> GetAssemblyListAsync()
        {
            var assemblies = await _assembliesCollection.Find(_ => true).ToListAsync();
            if (assemblies == null || assemblies.Count == 0)
            {
                throw new NotFoundException(nameof(assemblies));
            }

            var result = _mapper.Map<List<CustomAssemblyDto?>>(assemblies);

            return result;
        }

        public async Task<List<CustomAssemblyDto?>> GetAssemblyListByUserIdAsync(Guid userId, string category = "PC")
        {
            var filter = Builders<CustomAssembly>.Filter.Eq(a => a.UserId, userId);
            if (category != null) 
            {
                filter = filter & Builders<CustomAssembly>.Filter.Eq(a => a.AssemblyCategory, category);
            }

            var assemblies = await _assembliesCollection.Find(filter).ToListAsync();
            if (assemblies == null || assemblies.Count == 0)
            {
                throw new NotFoundException(nameof(assemblies));
            }

            var result = _mapper.Map<List<CustomAssemblyDto?>>(assemblies);

            return result;
        }

        public async Task<List<Guid>?> GetComponentIdsByAssemblyIdAsync(Guid assemblyId)
        {
            var filter = Builders<CustomAssembly>.Filter.Eq(a => a.Id, assemblyId);

            var assembly = await _assembliesCollection.Find(filter).FirstOrDefaultAsync();
            if (assembly == null)
            {
                throw new NotFoundException(nameof(assembly));
            }

            return assembly.ComponentIds.ToList();
        }

        public async Task<bool> RemoveAssemblyAsync(Guid assemblyId)
        {
            var filter = Builders<CustomAssembly>.Filter.Eq(a => a.Id, assemblyId);

            var assembly = await _assembliesCollection.Find(filter).FirstOrDefaultAsync();
            if (assembly == null)
            {
                throw new NotFoundException(nameof(assembly));
            }

            var deleteResult = await _assembliesCollection.DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateAssemblyAsync(CustomAssemblyDto assemblyToUpdate)
        {
            var filter = Builders<CustomAssembly>.Filter.Eq(a => a.Id, assemblyToUpdate.Id);

            var assembly = await _assembliesCollection.Find(filter).FirstOrDefaultAsync();
            if (assembly == null)
            {
                throw new NotFoundException(nameof(assembly));
            }

            assembly.ComponentIds = assemblyToUpdate.ComponentIds;
            var updateResult = await _assembliesCollection.ReplaceOneAsync(filter, assembly);
            
            return updateResult.ModifiedCount > 0;
        }
    }
}
