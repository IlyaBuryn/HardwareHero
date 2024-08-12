using HardwareHero.Filter.Operations;

namespace Contributor.BusinessLogic.Filters
{
    public class ChatRoomFilter : FilterRequestDomain<ChatRoom>, IPaginable
    {
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
    }
}
