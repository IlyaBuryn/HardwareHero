namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentLocalReview : ReviewBase
    {
        public Guid UserId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
