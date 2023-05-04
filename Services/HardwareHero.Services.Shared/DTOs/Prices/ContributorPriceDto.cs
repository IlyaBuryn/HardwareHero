using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Services.Shared.DTOs.Prices
{
    public class ContributorPriceDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public Guid ContributorId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Pricestamp { get; set; }
    }
}
