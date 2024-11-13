using HardwareHero.Filter.Operations;

namespace Contributor.BusinessLogic.Filters
{
    public class ContributorsFilter : IPaginable
    {
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public string? Currency { get; set; }
        public int? PriorityLevel { get; set; }
        public bool? IsActiveSubscriber { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsWithoutConfirmedStatus { get; set; }

        public bool ShowOnlyExcellences { get; set; } = false;

        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
    }
}
