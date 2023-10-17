namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class MaintenanceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid MaintenanceTypeId { get; set; }
        public Guid ContributorId { get; set; }
        public MaintenanceTypeDto? MaintenanceType { get; set; }
    }
}
