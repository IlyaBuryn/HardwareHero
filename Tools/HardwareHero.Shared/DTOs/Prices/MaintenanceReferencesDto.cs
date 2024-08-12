namespace HardwareHero.Shared.DTOs.Prices
{
    public class MaintenanceReferencesDto
    {
        public Guid Id { get; set; }
        public Guid MaintenanceId { get; set; }
        public Guid ContributorId { get; set; }
        public string Link { get; set; }
    }
}
