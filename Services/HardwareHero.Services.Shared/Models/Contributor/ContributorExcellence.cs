using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class ContributorExcellence : BaseEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string? Description { get; set; }
        public string Phone { get; set; }
        public string? MainWebLink { get; set; }
        public string? MainApiLink { get; set; }
        public Guid RegionId { get; set; }
        public Guid? CurrencyId { get; set; }

        public virtual Currency? Currency { get; set; }
        public virtual Region? Region { get; set; }
    }
}