using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class ConfiguratorService : IConfiguratorService
    {
        private readonly IMongoCollection<ConfiguratorRule> _rulesCollection;
        private readonly IMongoCollection<ConfiguratorComponent> _componentsCollection;
        private readonly DatabaseOptions _databaseSettings;
        private readonly IMapper _mapper;

        public ConfiguratorService(IMapper mapper, IOptions<DatabaseOptions> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _rulesCollection = mongoDb
                .GetCollection<ConfiguratorRule>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.ConfiguratorRulesCollection].CollectionName);

            _componentsCollection = mongoDb
                .GetCollection<ConfiguratorComponent>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.ComponentsCollection].CollectionName);

            _mapper = mapper;
            _databaseSettings = databaseSettings.Value;
        }

        //public async Task<List<ConfiguratorComponentDto>?> GetFilteredComponentsAsync
        //    (List<ConfiguratorComponentDto>? currentAssembly, string targetType, bool filterEnable = true)
        //{
        //    // фильтры для этого типа компонентов
        //    var filters = new List<ConfiguratorComponentAttribute>();

        //    // Получить правила для объекта targetType (Например MB)
        //    var targetRules = await _rulesCollection.Find(x => x.TargetComponentType == targetType).ToListAsync();

        //    // Проход по всем правилам для target MB (первый будет MB->CPU)
        //    foreach (var rule in targetRules)
        //    {
        //        // если текущая сборка пустая то возвращаем все результаты для выбора
        //        if (currentAssembly == null || currentAssembly.Count == 0)
        //        {
        //            var result = await _componentsCollection.Find(x => x.ComponentType == targetType).ToListAsync();

        //            return _mapper.Map<List<ConfiguratorComponentDto>>(result);
        //        }

        //        // по этому правилу находится один компонент связанный target->source и только один так как больше нету (CPU)
        //        var sourceComponent = currentAssembly.FirstOrDefault(c => c.ComponentType == rule.SourceComponentType);
        //        if (sourceComponent != null)
        //        {
        //            // далее получаем у этого компонента атрибуты, а именно:
        //            //{
        //            //  "TargetComponentType": "MB",
        //            //  "TargetAttributeName": "Memory Standard",
        //            //  "SourceComponentType": "CPU",
        //            //  "SourceAttributeName": "Memory Type",
        //            //  "ProblemLevel": 2
        //            //}, ->
        //            // {
        //            //  "Key": "Memory Type",
        //            //  "Value": "DDR5",
        //            //  "Operation": "values"
        //            //},
        //            var attribute = sourceComponent.Attributes.FirstOrDefault(a => a.Key == rule.SourceAttributeName);
        //            if (attribute != null)
        //            {
        //                filters.Add(new ConfiguratorComponentAttribute
        //                {
        //                    Key = rule.TargetAttributeName,
        //                    Value = attribute.Value,
        //                    Operation = attribute.Operation,
        //                });
        //            }
        //        }
        //    }
        //}

    }
}
