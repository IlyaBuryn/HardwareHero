using HardwareHero.Services.Shared.Infrastructure.Reviews;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class MaintenanceGlobalReview : GlobalReviewBase
    {
        public Guid ContributorId { get; set; }
        public Guid MaintenanceId { get; set; }

        public virtual Maintenance? Maintenance { get; set; }
    }
}
