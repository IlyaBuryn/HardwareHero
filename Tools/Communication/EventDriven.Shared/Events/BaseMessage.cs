namespace EventDriven.Shared.Events
{
    public class BaseMessage : BaseEvent
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public string? Endpoint { get; set; }
    }
}
