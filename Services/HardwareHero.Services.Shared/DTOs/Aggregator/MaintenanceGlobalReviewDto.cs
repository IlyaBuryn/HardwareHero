using HardwareHero.Services.Shared.Infrastructure.Reviews;

namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class MaintenanceGlobalReviewDto : GlobalReviewBase
    {
        public Guid Id { get; set; }
        public Guid ContributorId { get; set; }
        public Guid MaintenanceId { get; set; }
        public MaintenanceDto? Maintenance { get; set; }
    }
}
