using EventDriven.Shared.Events;

namespace References.DTOs.Messages
{
    public class RegionRequestMessage : BaseMessage
    {
        public Guid? ByRegionId { get; set; }
        public string? ByCode { get; set; }
        public string? ByCountry { get; set; }
    }
}
