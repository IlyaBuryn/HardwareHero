namespace Contributor.BusinessLogic.Filters
{
    public class ContributorsFilter : FilterRequestDomain<ContributorModel>
    {
        public ContributorsFilter()
            : base()
        {
            
        }

        public override void SetFilterExpression()
        {
            base.SetFilterExpression();

            if (CompanyName != null)
                AddFilterCondition(x => x.ContributorExcellence.Name.Contains(CompanyName));
            if (Phone != null)
                AddFilterCondition(x => x.ContributorExcellence.Phone.Contains(Phone));
            if (Country != null)
                AddFilterCondition(x => x.ContributorExcellence.Region != null ? x.ContributorExcellence.Region.Country.Contains(Country) : true);
            if (Currency != null)
                AddFilterCondition(x => x.ContributorExcellence.Currency != null ? x.ContributorExcellence.Currency.Name.Contains(Currency) : true);
            if (PriorityLevel != null)
                AddFilterCondition(x => PriorityLevel != null ? x.SubscriptionPlanInfo.SubscriptionPlan.PriorityLevel == PriorityLevel : true);
            if (IsActiveSubscriber != null)
                AddFilterCondition(x => IsActiveSubscriber != null ? x.SubscriptionPlanInfo.RenewalDate > DateTime.UtcNow : true);
            if (IsConfirmed != null)
                AddFilterCondition(x => IsConfirmed != null ? x.ContributorConfirmInfo.IsConfirmed == IsConfirmed : true);
            if (IsWithoutConfirmedStatus != null)
                AddFilterCondition(x => IsWithoutConfirmedStatus == true ? x.ContributorConfirmInfo == null : true);
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
    }
}
