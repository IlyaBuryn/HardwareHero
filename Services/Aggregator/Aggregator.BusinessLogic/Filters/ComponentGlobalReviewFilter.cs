using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentGlobalReviewFilter : Filter<ComponentGlobalReview>
    {
        public override ComponentGlobalReview SelectionPattern(ComponentGlobalReview refItem)
        {
            refItem.Component = null;
            return refItem;
        }
    }
}
