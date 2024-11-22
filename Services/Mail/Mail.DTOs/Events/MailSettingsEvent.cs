using EventDriven.Shared.Events;

namespace Mail.DTOs.Events
{
    public class MailSettingsEvent : BaseEvent
    {
        public string Username { get; set; }
        public string RecipientMailAddress { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
