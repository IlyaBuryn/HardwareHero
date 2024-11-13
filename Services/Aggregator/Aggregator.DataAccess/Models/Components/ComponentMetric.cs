using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Components
{
    public class ComponentMetric : BaseEntity
    {
        public Guid? ComponentId { get; set; }
        public int ViewsCount { get; set; }
        public int ReviewCount { get; set; }
        public double Rating { get; set; }
        public decimal MinPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Component? Component { get; set; }
    }
}
