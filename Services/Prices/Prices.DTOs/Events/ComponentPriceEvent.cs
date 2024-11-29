using EventDriven.Shared.Events;

namespace Prices.DTOs.Events
{
    public class ComponentPriceEvent : BaseMessage
    {
        public Guid ComponentId { get; set; }
    }
}
