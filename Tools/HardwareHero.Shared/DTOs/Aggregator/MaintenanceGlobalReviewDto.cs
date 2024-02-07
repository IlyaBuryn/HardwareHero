using HardwareHero.Shared.Models.Reviews;

namespace HardwareHero.Shared.DTOs.Aggregator
{
    public class MaintenanceGlobalReviewDto : GlobalReviewBase
    {
        public Guid Id { get; set; }
        public Guid ContributorId { get; set; }
        public Guid MaintenanceId { get; set; }
        public MaintenanceDto? Maintenance { get; set; }
    }
}
