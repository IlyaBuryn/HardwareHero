using HardwareHero.Filter.RequestsModels;
using HardwareHero.Services.Shared.Models.Contributor;

namespace Contributor.BusinessLogic.Filters
{
    public class ContributorsFilter : FilterRequestDomain<ContributorModel>
    {
        public ContributorsFilter()
            : base()
        {
            AddExpression(x => x.ContributorExcellence.Name.Contains(CompanyName));
            AddExpression(x => x.ContributorExcellence.Phone.Contains(Phone));
            AddExpression(x => x.ContributorExcellence.Region != null ? x.ContributorExcellence.Region.Country.Contains(Country) : true);
            AddExpression(x => x.ContributorExcellence.Currency != null ? x.ContributorExcellence.Currency.Name.Contains(Currency) : true);

            AddExpression(x => PriorityLevel != null ? x.SubscriptionPlanInfo.SubscriptionPlan.PriorityLevel == PriorityLevel : true);
            AddExpression(x => IsActiveSubscriber != null ? x.SubscriptionPlanInfo.RenewalDate > DateTime.UtcNow : true);
            AddExpression(x => IsConfirmed != null ? x.ContributorConfirmInfo.IsConfirmed == IsConfirmed : true);

            AddExpression(x => IsWithoutConfirmedStatus == true ? x.ContributorConfirmInfo == null : true);
        }

        public string? CompanyName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? Currency { get; set; } = string.Empty;
        public int? PriorityLevel { get; set; }
        public bool? IsActiveSubscriber { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsWithoutConfirmedStatus { get; set; }

        public bool ShowOnlyExcellences { get; set; } = false;
    }
}
