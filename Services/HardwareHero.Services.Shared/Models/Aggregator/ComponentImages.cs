namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentImages : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public string Image { get; set; }

        public virtual Component? Component { get; set; }
    }
}
