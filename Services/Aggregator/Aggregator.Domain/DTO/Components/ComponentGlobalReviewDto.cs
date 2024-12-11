using Aggregator.DataAccess.Models.Reviews;

namespace Aggregator.Domain.Components.DTO
{
    public class ComponentGlobalReviewDto : GlobalReviewBase
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public Guid ContributorId { get; set; }
        public Guid ComponentId { get; set; }
    }
}
