using EventDriven.Shared.Events;
using Identity.Shared.Requests;

namespace Identity.Shared.Events
{
    public class TokenRequestEvent : BaseMessage
    {
        public TokenRequest? Tokens { get; set; }
    }
}
