namespace HardwareHero.Shared.Models.Aggregator
{
    public class ComponentViews : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public int ViewsCount { get; set; }

        public virtual Component? Component { get; set; }
    }
}
