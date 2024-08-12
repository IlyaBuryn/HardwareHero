using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Shared.Models.Prices
{
    public class MaintenanceReferences
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid MaintenanceId { get; set; }
        public Guid ContributorId { get; set; }
        public string Link { get; set; }
    }
}
