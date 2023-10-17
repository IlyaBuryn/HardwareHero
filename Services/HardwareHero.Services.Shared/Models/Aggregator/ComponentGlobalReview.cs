namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentGlobalReview : ReviewBase
    {
        public string AuthorName { get; set; }
        public Guid ComponentId { get; set; }
        public Guid ContributorId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
