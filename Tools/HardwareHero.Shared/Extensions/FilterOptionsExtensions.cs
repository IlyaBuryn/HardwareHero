using HardwareHero.Filter.Operations;

namespace HardwareHero.Shared.Extensions
{
    public static class FilterOptionsExtensions
    {
        public static bool IsWrongPageOptions(this IPaginable? filter)
            => filter == null || filter.PageNumber == 0 || filter.PageSize == 0;
    }
}
