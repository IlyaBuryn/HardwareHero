using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class Region : BaseEntity
    {
        public string Country { get; set; }
        public string? City { get; set; }
    }
}
