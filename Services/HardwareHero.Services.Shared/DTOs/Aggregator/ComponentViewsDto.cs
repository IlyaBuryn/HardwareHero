namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class ComponentViewsDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public int ViewsCount { get; set; }
        public ComponentDto? Component { get; set; }
    }
}
