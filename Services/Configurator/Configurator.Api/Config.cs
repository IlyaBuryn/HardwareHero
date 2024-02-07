using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;

namespace Configurator.Api
{
    public class Config
    {
        private readonly IComponentTypesService _componentTypesService;

        public Config(IComponentTypesService componentTypesService)
        {
            _componentTypesService = componentTypesService;
        }

        public async Task<bool> SeedDatabaseAsync()
        {
            ConfigureOptions();

            Type baseClass = typeof(ComponentTypeSigns);
            var components = Assembly.GetAssembly(baseClass)!.GetTypes().Where(type => type.IsSubclassOf(baseClass));

            var listToSeed = new List<ComponentTypeSigns>();

            foreach (var component in components)
            {
                var instance = (ComponentTypeSigns)Activator.CreateInstance(component);
                var seedToAdd = instance!.ConfigureSpecificDescription();
                listToSeed.Add(seedToAdd);
            }

            var result = await _componentTypesService.SeedDatabaseAsync(listToSeed);

            if (result != null && result.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void ConfigureOptions()
        {
            var pack = new ConventionPack
            {
                new IgnoreIfNullConvention(true)
            };
            ConventionRegistry.Register("IgnoreIfNull", pack, t => true);
        }
    }
}
