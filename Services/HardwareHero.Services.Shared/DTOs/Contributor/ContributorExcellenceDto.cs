namespace HardwareHero.Services.Shared.DTOs.Contributor
{
    public class ContributorExcellenceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public byte[] ImageData { get; set; }
        public string? Description { get; set; }
        public string Phone { get; set; }
        public string? MainWebLink { get; set; }
        public string? MainApiLink { get; set; }
        public Guid RegionId { get; set; }
        public Guid? CurrencyId { get; set; }

        public CurrencyDto? Currency { get; set; }
        public RegionDto? Region { get; set; }
    }
}
