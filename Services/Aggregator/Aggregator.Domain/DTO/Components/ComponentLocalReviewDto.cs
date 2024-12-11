using Aggregator.DataAccess.Models.Reviews;

namespace Aggregator.Domain.Components.DTO
{
    public class ComponentLocalReviewDto : LocalReviewBase
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public Guid ComponentId { get; set; }
    }
}
