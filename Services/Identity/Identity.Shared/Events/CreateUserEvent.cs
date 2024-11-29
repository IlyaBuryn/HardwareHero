using EventDriven.Shared.Events;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Shared.Events
{
    public class CreateUserEvent : BaseMessage
    {
        public CreateUserRequest? Model { get; set; }
    }
}
