using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Shared.Models.Prices
{
    public class ComponentReferences
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public string Link { get; set; }
    }
}
