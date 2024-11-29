using EventDriven.Shared.Events;
using Identity.Shared.Responses;

namespace Identity.Shared.Events
{
    public class AuthResultEvent : BaseMessage
    {
        public AuthenticationResponse? AuthenticationResponse { get; set; }
    }
}
