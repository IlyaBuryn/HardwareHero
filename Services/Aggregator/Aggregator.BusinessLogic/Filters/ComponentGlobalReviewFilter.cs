using Aggregator.DataAccess.Models.Components;
using HardwareHero.Filter.Operations;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentGlobalReviewFilter : FilterRequestDomain<ComponentGlobalReview>,
        ISelectable<ComponentGlobalReview>, IPaginable
    {
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }

        public object SetupSelectFields(ComponentGlobalReview? item)
        {
            return new ComponentGlobalReview
            {
                Id = item.Id,
                Text = item.Text,
                AuthorName = item.AuthorName,
                ComponentId = item.ComponentId,
                ContributorId = item.ContributorId,
                IsRecommended = item.IsRecommended,
                Timestamp = item.Timestamp,
            };
        }
    }
}
