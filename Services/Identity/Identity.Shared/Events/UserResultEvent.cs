using EventDriven.Shared.Events;
using Identity.Shared.Domain;

namespace Identity.Shared.Events
{
    public class UserResultEvent : BaseMessage
    {
        public ApplicationUser? User { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
