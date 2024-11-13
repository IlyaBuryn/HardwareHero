using Aggregator.DataAccess.Models.Reviews;

namespace Aggregator.DataAccess.Models.Components
{
    public class ComponentGlobalReview : GlobalReviewBase
    {
        public Guid ContributorId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual Component? Component { get; set; }
    }
}
