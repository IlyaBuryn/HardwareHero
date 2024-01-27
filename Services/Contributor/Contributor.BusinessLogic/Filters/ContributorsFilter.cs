using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Contributor;

namespace Contributor.BusinessLogic.Filters
{
    public class ContributorsFilter : Filter<ContributorModel>
    {
        [FilterProperty("ContributorExcellence.Name", FilterOperation.Contains)]
        public string? CompanyName { get; set; }
        [FilterProperty("ContributorExcellence.Phone", FilterOperation.Contains)]
        public string? Phone { get; set; }
        [FilterProperty("ContributorExcellence.Region.Country", FilterOperation.Contains)]
        public string? Country { get; set; }
        [FilterProperty("ContributorExcellence.Currency.Name", FilterOperation.Contains)]
        public string? Currency { get; set; }
        [FilterProperty("SubscriptionPlanInfo.SubscriptionPlan.PriorityLevel", FilterOperation.Equal)]
        public int? PriorityLevel { get; set; }
        [FilterProperty("SubscriptionPlanInfo.RenewalDate", FilterOperation.GreaterThanCurrentTime)]
        public bool? IsActiveSubscriber { get; set; }
        [FilterProperty("ContributorConfirmInfo.IsConfirmed", FilterOperation.Equal)]
        public bool? IsConfirmed { get; set; }
        [FilterProperty("ContributorConfirmInfo", FilterOperation.IsNull)]
        public bool? IsWithoutConfirmedStatus { get; set; }


        public bool ShowOnlyExcellences { get; set; } = false;
    }
}
