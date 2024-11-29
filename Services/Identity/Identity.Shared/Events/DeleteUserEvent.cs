using EventDriven.Shared.Events;

namespace Identity.Shared.Events
{
    public class DeleteUserEvent : BaseMessage
    {
        public string? UserId { get; set; }
    }
}
