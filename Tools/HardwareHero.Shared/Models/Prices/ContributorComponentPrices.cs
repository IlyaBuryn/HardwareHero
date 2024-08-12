using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Shared.Models.Prices
{
    public class ContributorComponentPrices
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public Guid ContributorId { get; set; }
        public List<PriceStamp> Prices { get; set; } = new();
    }
}
