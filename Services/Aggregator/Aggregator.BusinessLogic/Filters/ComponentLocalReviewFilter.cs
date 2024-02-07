namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentLocalReviewFilter : FilterRequestDomain<ComponentLocalReview>
    {
        public override ComponentLocalReview SelectionPattern(ComponentLocalReview refItem)
        {
            refItem.Component = null;
            return refItem;
        }
    }
}
