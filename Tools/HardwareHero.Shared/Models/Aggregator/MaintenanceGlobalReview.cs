using HardwareHero.Shared.Models.Reviews;

namespace HardwareHero.Shared.Models.Aggregator
{
    public class MaintenanceGlobalReview : GlobalReviewBase
    {
        public Guid ContributorId { get; set; }
        public Guid MaintenanceId { get; set; }

        public virtual Maintenance? Maintenance { get; set; }
    }
}
