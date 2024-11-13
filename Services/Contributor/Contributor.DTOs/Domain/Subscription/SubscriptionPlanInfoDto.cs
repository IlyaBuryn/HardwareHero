namespace Contributor.DTOs.Domain.Subscription
{
    public class SubscriptionPlanInfoDto
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public SubscriptionPlanDto? SubscriptionPlan { get; set; }
    }
}
