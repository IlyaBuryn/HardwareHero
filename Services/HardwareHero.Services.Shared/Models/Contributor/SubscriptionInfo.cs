namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class SubscriptionInfo : BaseEntity
    {
        public DateTime RenewalDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual SubscriptionPlan Plan { get; set; }
        public Guid PlanId { get; set; }
    }
}
