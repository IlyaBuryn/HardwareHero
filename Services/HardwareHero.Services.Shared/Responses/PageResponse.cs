using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Responses
{
    public class PageResponse<T>
    {
        public List<T>? Items { get; set; }
        public int TotalPages { get; set; }
        public PaginationInfo? CurrentPaginationInfo { get; set; }
    }
}
