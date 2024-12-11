namespace Contributor.Domain.DTO.Subscription
{
    public class ContributorSubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsDelayed { get; set; }

        public SubscriptionPlanDto? SubscriptionPlan { get; set; } 
            = new();
    }
}