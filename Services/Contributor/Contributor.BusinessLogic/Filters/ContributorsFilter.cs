using Contributor.DataAccess.Models;
using HardwareHero.Shared.Extensions.Filter;

namespace Contributor.BusinessLogic.Filters
{
    public class ContributorsFilter : IPaginableFilter<ContributorModel>
    {
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public string? Currency { get; set; }
        public int? PriorityLevel { get; set; }
        public bool? IsActiveSubscriber { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsWithoutConfirmedStatus { get; set; }

        public bool SortByFormer { get; set; }

        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }

        public Func<ContributorModel, bool> BuildFilterPredicate()
        {
            return x =>
            {
                bool matches = true;

                if (!string.IsNullOrEmpty(CompanyName))
                {
                    matches &= x.ContributorExcellence.Name.Contains(CompanyName, StringComparison.OrdinalIgnoreCase);
                }

                if (!string.IsNullOrEmpty(Phone))
                {
                    matches &= x.ContributorExcellence.Phone.Contains(Phone, StringComparison.OrdinalIgnoreCase);
                }

                if (!string.IsNullOrEmpty(Country) && x.ContributorExcellence.Region != null)
                {
                    matches &= x.ContributorExcellence.Region.Code.Contains(Country, StringComparison.OrdinalIgnoreCase) ||
                        x.ContributorExcellence.Region.Country.Contains(Country, StringComparison.OrdinalIgnoreCase);
                }

                if (!string.IsNullOrEmpty(Currency) && x.ContributorExcellence.Currency != null)
                {
                    matches &= x.ContributorExcellence.Currency.Code.Contains(Currency, StringComparison.OrdinalIgnoreCase);
                }

                if (PriorityLevel > 0 && x.SubscriptionPlanInfo != null && x.SubscriptionPlanInfo.SubscriptionPlan != null)
                {
                    matches &= x.SubscriptionPlanInfo.SubscriptionPlan.PriorityLevel == PriorityLevel;
                }

                if (IsActiveSubscriber != null && x.SubscriptionPlanInfo != null)
                {
                    matches &= x.SubscriptionPlanInfo.ExpiryDate < DateTime.UtcNow;
                }

                if (IsConfirmed != null && x.ContributorConfirmInfo != null)
                {
                    matches &= x.ContributorConfirmInfo.IsConfirmed == IsConfirmed;
                }

                if (IsWithoutConfirmedStatus != null)
                {
                    matches &= x.ContributorConfirmInfo == null;
                }

                return matches;
            };
        }

        public IQueryable<ContributorModel> ApplySorting(IQueryable<ContributorModel> query)
        {
            query = SortByFormer == true ? 
                query.OrderBy(x => x.ContributorApplicationDate) :
                query;

            return query;
        }
    }
}
