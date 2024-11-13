using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class SubscriptionPlan : BaseEntity
    {
        public decimal Price { get; set; }
        public Guid? CurrencyId { get; set; }
        public int DaysCount { get; set; }
        public int PriorityLevel { get; set; }

        public virtual Currency? Currency { get; set; }
    }
}
