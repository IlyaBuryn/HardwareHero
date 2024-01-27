using HardwareHero.Services.Shared.Infrastructure.Reviews;

namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class ComponentGlobalReviewDto : GlobalReviewBase
    {
        public Guid Id { get; set; }
        public Guid ContributorId { get; set; }
        public Guid ComponentId { get; set; }
        public ComponentDto? Component { get; set; }
    }
}
