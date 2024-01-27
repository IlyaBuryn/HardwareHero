using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentLocalReviewFilter : Filter<ComponentLocalReview>
    {
        public override ComponentLocalReview SelectionPattern(ComponentLocalReview refItem)
        {
            refItem.Component = null;
            return refItem;
        }
    }
}
