namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class Maintenance : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid MaintenanceTypeId { get; set; }
        public Guid ContributorId { get; set; }

        public virtual MaintenanceType? MaintenanceType { get; set; }
    }
}
