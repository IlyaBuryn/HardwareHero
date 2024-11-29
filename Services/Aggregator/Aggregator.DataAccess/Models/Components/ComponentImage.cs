using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Components
{
    public class ComponentImage : BaseEntity
    {
        // It's nullable due to simple find revoked images in the future
        public Guid? ComponentId { get; set; }
        public Guid? RevokedId { get; set; }

        // For images storage
        public string? ImageUrl { get; set; }
        public int Index { get; set; }

        public virtual Component? Component { get; set; }
    }
}
