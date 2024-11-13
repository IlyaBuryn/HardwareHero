using Aggregator.DataAccess.Models.Reviews;

namespace Aggregator.DTOs.Components
{
    public class ComponentLocalReviewDto : LocalReviewBase
    {
        public new Guid Id { get; set; }
        public Guid ComponentId { get; set; }
    }
}
