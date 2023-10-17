namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class ComponentLocalReviewDto : LocalReviewDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public ComponentDto? Component { get; set; }
    }
}
