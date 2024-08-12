using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Shared.Models.Configurator
{
    public class ConfiguratorComponent
    {
        [BsonId]
        public Guid ComponentId { get; set; }
        public string ComponentType { get; set; }
        public List<ConfiguratorComponentAttribute> Attributes { get; set; }
    }
}
