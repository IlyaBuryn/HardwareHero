namespace Contributor.DTOs.Domain.Regions
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string? City { get; set; }
    }
}
