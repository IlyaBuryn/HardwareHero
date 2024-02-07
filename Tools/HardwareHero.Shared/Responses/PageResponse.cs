namespace HardwareHero.Shared.Responses
{
    public class PageResponse<T>
    {
        public List<T>? Items { get; set; }
        public int TotalPages { get; set; }
        public PaginationInfo? CurrentPaginationInfo { get; set; }
    }
}
