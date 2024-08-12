using HardwareHero.Filter.Operations;

namespace Contributor.BusinessLogic.Filters
{
    public class MessagesFilter : FilterRequestDomain<ChatMessage>, IPaginable
    {
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
    }
}
