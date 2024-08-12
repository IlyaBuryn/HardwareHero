using HardwareHero.Shared.Models.Reviews;

namespace HardwareHero.Shared.Models.Aggregator
{
    public class ComponentGlobalReview : GlobalReviewBase
    {
        public Guid ContributorId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
