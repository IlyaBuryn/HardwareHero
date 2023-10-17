namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class ComponentGlobalReviewDto : GlobalReviewDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public ComponentDto? Component { get; set; }
    }
}
