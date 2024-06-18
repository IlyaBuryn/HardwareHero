namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentLocalReviewFilter : FilterRequestDomain<ComponentLocalReview>
    {
        public ComponentLocalReviewFilter()
            : base() 
        {
            AddTransformationPattern(SelectionPattern);
        }

        public static ComponentLocalReview? SelectionPattern(ComponentLocalReview? item)
        {
            item.Component = null;
            return item;
        }
    }
}
