namespace HardwareHero.Shared.Responses
{
    public class PageResponse<T>
    {
        public IEnumerable<T?>? Items { get; set; }
        public uint TotalPages { get; set; }
        public uint CurrentPageSize { get; set; }
        public uint CurrentPageNumber { get; set; }
        public int Count { get; set; }
    }
}
