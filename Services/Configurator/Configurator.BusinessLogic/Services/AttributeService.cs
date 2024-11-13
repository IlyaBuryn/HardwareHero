using Configurator.BusinessLogic.Models;
using Configurator.DTOs.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IMongoCollection<ConfiguratorComponent> _componentsCollection;
        private readonly DatabaseOptions _databaseSettings;
        private readonly IMapper _mapper;

        public AttributeService(IMapper mapper, IOptions<DatabaseOptions> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _componentsCollection = mongoDb
                .GetCollection<ConfiguratorComponent>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.ComponentsCollection].CollectionName);

            _mapper = mapper;
            _databaseSettings = databaseSettings.Value;
        }

        public List<AttributeGroupDto> GetAttributeGroups(string type)
        {
            var result = new Dictionary<string, AttributeGroupDto>();

            var filter = Builders<ConfiguratorComponent>.Filter.Eq(c => c.ComponentType, type);
            using (var cursor = _componentsCollection.Find(filter).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    var batch = cursor.Current;

                    foreach (var component in batch)
                    {
                        foreach (var attribute in component.Attributes)
                        {
                            if (!result.ContainsKey(attribute.Key))
                            {
                                result[attribute.Key] = new AttributeGroupDto
                                {
                                    Key = attribute.Key,
                                    Values = new List<string>(),
                                    DataType = "List"
                                };
                            }

                            var attributeGroup = result[attribute.Key];

                            var values = attribute.Value.Split(new[] { " / " }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var value in values)
                            {
                                if (!attributeGroup.Values.Contains(value))
                                {
                                    attributeGroup.Values.Add(value);
                                }
                            }

                            if (attribute.Operation == "max" && attributeGroup.DataType != "Range")
                            {
                                attributeGroup.DataType = "Range";
                            }
                        }
                    }
                }
            }

            return result.Values.ToList();
        }
    }
}
