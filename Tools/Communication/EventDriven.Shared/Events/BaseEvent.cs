using HardwareHero.Shared.Models;

namespace EventDriven.Shared.Events
{
    public class BaseEvent : BaseEntity
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
