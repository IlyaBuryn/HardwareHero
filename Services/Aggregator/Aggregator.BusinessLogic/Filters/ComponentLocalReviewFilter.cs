using HardwareHero.Filter.Operations;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentLocalReviewFilter : FilterRequestDomain<ComponentLocalReview>,
        ISelectable<ComponentLocalReview>, IPaginable
    {
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }

        public object SetupSelectFields(ComponentLocalReview? item)
        {
            return new ComponentLocalReview
            {
                Id = item.Id,
                Text = item.Text,
                Rating = item.Rating,
                ComponentId = item.ComponentId,
                IsRecommended = item.IsRecommended,
                Timestamp = item.Timestamp,
                UserId = item.UserId
            };
        }
    }
}
