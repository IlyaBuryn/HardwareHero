using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class Region : BaseEntity
    {
        public string Country { get; set; }
        public string Code { get; set; }
        public string? City { get; set; }

        // TODO: Probably, I can put here "exchange rate" thing and bind every currency with its country
    }
}
