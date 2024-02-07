namespace HardwareHero.Shared.Models.Aggregator
{
    public class ComponentAttributes : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

        public virtual Component? Component { get; set; }
    }
}
