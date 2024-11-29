using EventDriven.Shared.Events;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Shared.Events
{
    public class UpdateUserEvent : BaseMessage
    {
        public UpdateUserRequest? Model { get; set; }
    }
}
