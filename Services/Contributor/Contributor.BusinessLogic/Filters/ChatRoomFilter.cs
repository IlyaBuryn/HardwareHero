using HardwareHero.Filter.Operations;

namespace Contributor.BusinessLogic.Filters
{
    public class ChatRoomFilter : IPaginable
    {
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
    }
}
