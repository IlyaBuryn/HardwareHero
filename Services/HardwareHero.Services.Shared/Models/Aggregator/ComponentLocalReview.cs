using HardwareHero.Services.Shared.Infrastructure.Reviews;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentLocalReview : LocalReviewBase
    {
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
