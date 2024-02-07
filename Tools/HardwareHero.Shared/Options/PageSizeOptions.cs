using HardwareHero.Filter.Options;

namespace HardwareHero.Shared.Options
{
    public class PageSizeOptions : IPageSizeOptions
    {
        public int PageSize { get; set; }
        public int DefaultPageNumber { get; set; }
    }
}
