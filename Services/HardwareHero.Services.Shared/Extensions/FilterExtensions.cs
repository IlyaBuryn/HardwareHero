using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Options;

namespace HardwareHero.Services.Shared.Extensions
{
    public static class FilterExtensions
    {
        public static void ApplyPageSizeOptions<T>(this Filter<T> filter, PageSizeOptions options)
        {
            if (filter.PaginationInfo.PageSize <= 0)
            {
                filter.PaginationInfo.PageSize = options.PageSize;
                filter.PaginationInfo.PageNumber = 1;
            }
        }
    }
}
