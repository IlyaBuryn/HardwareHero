using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Shared.Models.Configurator
{
    public class StoredAssembly
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public List<ConfiguratorComponent> SelectedComponents { get; set; }
    }
}
