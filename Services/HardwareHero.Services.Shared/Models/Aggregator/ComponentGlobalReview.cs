using HardwareHero.Services.Shared.Infrastructure.Reviews;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentGlobalReview : GlobalReviewBase
    {
        public Guid ContributorId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
