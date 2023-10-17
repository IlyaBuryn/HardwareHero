namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class MaintenanceLocalReviewDto : LocalReviewDto
    {
        public Guid Id { get; set; }
        public Guid MaintenanceId { get; set; }
        public MaintenanceDto? Maintenance { get; set; }
    }
}
