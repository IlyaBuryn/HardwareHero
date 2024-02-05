using HardwareHero.Filter.RequestsModels;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentGlobalReviewFilter : FilterRequestDomain<ComponentGlobalReview>
    {
        public override ComponentGlobalReview SelectionPattern(ComponentGlobalReview refItem)
        {
            refItem.Component = null;
            return refItem;
        }
    }
}
