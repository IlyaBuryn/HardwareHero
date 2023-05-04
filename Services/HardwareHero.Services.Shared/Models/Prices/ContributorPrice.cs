using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Services.Shared.Models.Prices
{
    public class ContributorPrice
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("Component")]
        public Guid ComponentId { get; set; }
        [BsonElement("Contributor")]
        public Guid ContributorId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Pricestamp { get; set; }
    }
}
