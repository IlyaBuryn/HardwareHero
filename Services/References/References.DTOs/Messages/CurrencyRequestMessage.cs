using EventDriven.Shared.Events;

namespace References.DTOs.Messages
{
    public class CurrencyRequestMessage : BaseMessage
    {
        public Guid? ByCurrencyId { get; set; }
        public string? ByCode { get; set; }
        public string? BySymbol { get; set; }
    }
}
