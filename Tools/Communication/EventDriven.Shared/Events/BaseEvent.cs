namespace EventDriven.Shared.Events
{
    public class BaseEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
