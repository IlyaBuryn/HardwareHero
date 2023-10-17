
namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class MaintenanceGlobalReview : ReviewBase
    {
        public string AuthorName { get; set; }
        public Guid MaintenanceId { get; set; }
        public Guid ContributorId { get; set; }

        public virtual Maintenance? Maintenance { get; set; }
    }
}
