using EventDriven.Shared.Events;
using Mail.DTOs.Events;

namespace Identity.Shared.Events
{
    public class FindUserToMailSagaEvent : BaseEvent
    {
        public string? UserId { get; set; }
        public SendMailEvent? Message { get; set; }
    }
}
