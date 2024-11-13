using HardwareHero.Filter.Operations;

namespace Users.Api.Filters
{
    public class UsersFilter : IPaginable
    {
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
    }
}
