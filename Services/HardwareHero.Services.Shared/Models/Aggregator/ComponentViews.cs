using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentViews : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public int ViewsCount { get; set; }

        public virtual Component? Component { get; set; }
    }
}
