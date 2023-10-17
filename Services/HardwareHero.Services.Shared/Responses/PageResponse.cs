namespace HardwareHero.Services.Shared.Responses
{
    public class PageResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        // public int CurrentPage { get; set; }
    }
}
