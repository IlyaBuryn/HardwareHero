using EventDriven.Shared.Events;

namespace References.DTOs.Domain
{
    public class RegionDto : BaseMessage
    {
        public RegionDto() { }

        public RegionDto(Exception exception) : base(exception) { }

        public string? Country { get; set; }
        public string? Code { get; set; }
        public string? City { get; set; }
    }
}
