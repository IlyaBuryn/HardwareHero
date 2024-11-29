using EventDriven.Shared.Events;

namespace Prices.DTOs.Events
{
    public class LatestLowestComponentPriceEvent : BaseMessage
    {
        public Guid ContributorId { get; set; }
        public decimal LowestPrice { get; set; }
    }
}
