namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentGlobalReviewFilter : FilterRequestDomain<ComponentGlobalReview>
    {
        public ComponentGlobalReviewFilter()
            : base()
        {
            AddTransformationPattern(SelectionPattern);
        }

        public static ComponentGlobalReview? SelectionPattern(ComponentGlobalReview? item)
        {
            item.Component = null;
            return item;
        }
    }
}
