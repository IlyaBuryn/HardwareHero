using EventDriven.Shared.Events;

namespace References.DTOs.Domain
{
    public class CurrencyDto : BaseMessage
    {
        public CurrencyDto() { }

        public CurrencyDto(Exception exception) : base(exception) { }

        public string? Code { get; set; }
        public string? Symbol { get; set; }
    }
}
