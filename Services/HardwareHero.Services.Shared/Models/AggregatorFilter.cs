using HardwareHero.Services.Shared.Options;

namespace HardwareHero.Services.Shared.Models
{
    public class AggregatorFilter
    {
        public string? SearchString { get; set; }
        public Dictionary<string, string>? AttributeFilters { get; set; }
        public string? Type { get; set; }
        public PaginationInfo paginationInfo { get; set; } 
    }
}
