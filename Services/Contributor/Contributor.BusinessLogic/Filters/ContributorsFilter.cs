using HardwareHero.Filter.Operations;
using System.Linq.Expressions;

namespace Contributor.BusinessLogic.Filters
{
    public class ContributorsFilter : FilterRequestDomain<ContributorModel>,
        IFilterable<ContributorModel>, ISortable<ContributorModel>, IPaginable
    {
        public ContributorsFilter()
        {
            SetupFilterExpressions();
            SetupSortByExpressions();
        }

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
        public string? SortByProperty { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public bool SortByDescending { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public void SetupFilterExpressions()
        {
            FilterExpressions[nameof(ContributorModel)] =
                contr =>
                (string.IsNullOrEmpty(CompanyName) || contr.ContributorExcellence.Name.Contains(CompanyName)) &&
                (string.IsNullOrEmpty(Phone) || contr.ContributorExcellence.Phone.Contains(Phone)) &&
                ((string.IsNullOrEmpty(Country) || contr.ContributorExcellence.Region == null) || contr.ContributorExcellence.Region.Country.Contains(Country)) &&
                ((string.IsNullOrEmpty(Currency) || contr.ContributorExcellence.Currency == null) || contr.ContributorExcellence.Currency.Name.Contains(Currency)) &&
                ((PriorityLevel == null || contr.SubscriptionPlanInfo == null || contr.SubscriptionPlanInfo.SubscriptionPlan == null)
                    || contr.SubscriptionPlanInfo.SubscriptionPlan.PriorityLevel == PriorityLevel) &&
                ((IsActiveSubscriber == null || contr.SubscriptionPlanInfo == null) || contr.SubscriptionPlanInfo.RenewalDate > DateTime.UtcNow) &&
                ((IsConfirmed == null || contr.ContributorConfirmInfo == null) || contr.ContributorConfirmInfo.IsConfirmed == IsConfirmed) &&
                ((IsWithoutConfirmedStatus == null || IsWithoutConfirmedStatus == false) || contr.ContributorConfirmInfo == null); 
        }

        public void SetupSortByExpressions()
        {
            SortByExpressions["Id"] = c => c.Id;
            SortByExpressions["ContributorConfirmInfo.IsConfirmed"] = c => c.ContributorConfirmInfo.IsConfirmed;
            SortByExpressions["ContributorConfirmInfo.TimeStamp"] = c => c.ContributorConfirmInfo.TimeStamp;
            SortByExpressions["UserId"] = c => c.UserId;
            SortByExpressions["SubscriptionPlanInfo.ExpiryDate"] = c => c.SubscriptionPlanInfo.ExpiryDate;
            SortByExpressions["SubscriptionPlanInfo.RenewalDate"] = c => c.SubscriptionPlanInfo.RenewalDate;
            SortByExpressions["RegionId"] = c => c.ContributorExcellence.RegionId;
            SortByExpressions["CurrencyId"] = c => c.ContributorExcellence.CurrencyId;
            SortByExpressions["Name"] = c => c.ContributorExcellence.Name;
        }

        public Expression<Func<ContributorModel, bool>>? OnGetFilterExpression()
            => GetFilterExpression(nameof(ContributorModel));

        public Expression<Func<ContributorModel, object>>? OnGetSortExpression(string? sortByProperty)
            => GetSortExpression(sortByProperty);
    }
}
