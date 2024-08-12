namespace HardwareHero.Shared.Responses
{
    public class PageResponse<T>
    {
        public List<T>? Items { get; set; }
        public uint TotalPages { get; set; }
        public uint CurrentPageSize { get; set; }
        public uint CurrentPageNumber { get; set; }
    }
}
