using HardwareHero.Shared.Models.Reviews;

namespace HardwareHero.Shared.Models.Aggregator
{
    public class ComponentLocalReview : LocalReviewBase
    {
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
