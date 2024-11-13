using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Reviews
{
    public class BaseReview : BaseEntity
    {
        public DateTime? Timestamp { get; set; }
        public bool? IsRecommended { get; set; }
        public string Text { get; set; }
    }
}
