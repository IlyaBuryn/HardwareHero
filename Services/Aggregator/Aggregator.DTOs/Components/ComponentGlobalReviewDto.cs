using Aggregator.DataAccess.Models.Reviews;

namespace Aggregator.DTOs.Components
{
    public class ComponentGlobalReviewDto : GlobalReviewBase
    {
        public new Guid Id { get; set; }
        public Guid ContributorId { get; set; }
        public Guid ComponentId { get; set; }
    }
}
