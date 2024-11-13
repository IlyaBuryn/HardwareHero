using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class SubscriptionPlanInfo : BaseEntity
    {
        public Guid PlanId { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual SubscriptionPlan? SubscriptionPlan { get; set; }
    }
}
