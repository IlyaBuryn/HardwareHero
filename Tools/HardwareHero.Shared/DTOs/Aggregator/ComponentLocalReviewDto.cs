using HardwareHero.Shared.Models.Reviews;

namespace HardwareHero.Shared.DTOs.Aggregator
{
    public class ComponentLocalReviewDto : LocalReviewBase
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public ComponentDto? Component { get; set; }
    }
}
