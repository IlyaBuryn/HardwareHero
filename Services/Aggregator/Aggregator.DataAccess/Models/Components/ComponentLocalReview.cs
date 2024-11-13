using Aggregator.DataAccess.Models.Reviews;

namespace Aggregator.DataAccess.Models.Components
{
    public class ComponentLocalReview : LocalReviewBase
    {
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
