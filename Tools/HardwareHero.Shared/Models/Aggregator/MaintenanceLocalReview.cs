using HardwareHero.Shared.Models.Reviews;

namespace HardwareHero.Shared.Models.Aggregator
{
    public class MaintenanceLocalReview : LocalReviewBase
    {
        public Guid MaintenanceId { get; set; }

        public virtual Maintenance? Maintenance { get; set; }
    }
}
