namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class MaintenanceLocalReview : ReviewBase
    {
        public Guid UserId { get; set; }
        public Guid MaintenanceId { get; set; }

        public virtual Maintenance? Maintenance { get; set; }
    }
}
