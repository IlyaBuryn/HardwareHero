using HardwareHero.Services.Shared.Infrastructure.Reviews;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class MaintenanceLocalReview : LocalReviewBase
    {
        public Guid MaintenanceId { get; set; }

        public virtual Maintenance? Maintenance { get; set; }
    }
}
