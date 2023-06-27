using AutoMapper;
using Configurator.BusinessLogic.Components;
using Configurator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Configurator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class ComponentTypesService : IComponentTypesService
    {
        private readonly IMongoCollection<ComponentTypeSigns> _componentTypeCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseOptions _databaseSettings;

        public ComponentTypesService(
            IMapper mapper,
            IOptions<DatabaseOptions> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _componentTypeCollection = mongoDb
                .GetCollection<ComponentTypeSigns>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.ComponentTypesCollection]
                .CollectionName);

            _mapper = mapper;
            _databaseSettings = databaseSettings.Value;
        }

        public async Task<Guid?> AddComponentTypeSignsAsync(ComponentTypeSignsDto signsToCreate)
        {
            if (signsToCreate.Id == Guid.Empty)
            {
                signsToCreate.Id = Guid.NewGuid();
            }

            var signs = _mapper.Map<ComponentTypeSigns>(signsToCreate);
            await _componentTypeCollection.InsertOneAsync(signs);

            return signsToCreate.Id;
        }

        public async Task<bool> UpdateComponentTypeSignsAsync(ComponentTypeSignsDto signsToUpdate)
        {
            var filter = Builders<ComponentTypeSigns>.Filter.Eq(a => a.Id, signsToUpdate.Id);

            var signs = await _componentTypeCollection.Find(filter).FirstOrDefaultAsync();
            if (signs == null)
            {
                throw new NotFoundException(nameof(signs));
            }

            signs.ComponentNames = signsToUpdate.ComponentNames;
            signs.Specifications = _mapper.Map<ComponentTypeSpecification[]>(signsToUpdate.Specifications);
            var updateResult = await _componentTypeCollection.ReplaceOneAsync(filter, signs);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<ComponentTypeSignsDto?> GetComponentTypeSignsByNameAsync(string name)
        {
            var filter = Builders<ComponentTypeSigns>.Filter.AnyEq(a => a.ComponentNames, name);

            var signs = await _componentTypeCollection.Find(filter).FirstOrDefaultAsync();
            if (signs == null)
            {
                throw new NotFoundException(nameof(signs));
            }

            var result = _mapper.Map<ComponentTypeSignsDto?>(signs);

            return result;
        }

        public async Task<List<ComponentTypeSignsDto>> SeedDatabaseAsync(List<ComponentTypeSigns> signsListToAdd)
        {
            foreach (var signs in signsListToAdd)
            {
                var filter = Builders<ComponentTypeSigns>.Filter.Eq(a => a.Id, signs.Id);
                
                var signsCheck = await _componentTypeCollection.Find(filter).FirstOrDefaultAsync();
                if (signsCheck == null)
                {
                    await _componentTypeCollection.InsertOneAsync(signs);
                }
                else
                {
                    await _componentTypeCollection.ReplaceOneAsync(filter, signs);
                }
            }

            return await GetComponentTypeSignsAsync();
        }

        public async Task<List<ComponentTypeSignsDto>> GetComponentTypeSignsAsync()
        {
            var documents = await _componentTypeCollection.Find(new BsonDocument()).ToListAsync();
            var result = _mapper.Map<List<ComponentTypeSignsDto>>(documents);
            
            return result;
        }
    }
}
