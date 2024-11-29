using EventDriven.Shared.Events;

namespace Mail.DTOs.Events
{
    public class SendMailEvent : BaseEvent
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? Username { get; set; }
        public string? Status { get; set; }
        public string? RecipientMailAddress { get; set; }
        public MailPreset MailPreset { get; set; } = 0;
        public Guid RecipientId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}