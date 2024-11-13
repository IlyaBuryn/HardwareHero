using Aggregator.DTOs.Components;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentReviewService
    {
        Task<Guid?> AddLocalReviewAsync(ComponentLocalReviewDto reviewToAdd);
        Task<bool> UpdateLocalReviewAsync(ComponentLocalReviewDto reviewToAdd);
        Task<bool> RemoveLocalReviewAsync(Guid reviewId);

        Task<Guid?> AddGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd);
        Task<bool> UpdateGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd);
        Task<bool> RemoveGlobalReviewAsync(Guid reviewId);

        Task<CreationOfManyResponse> AddGlobalReviewsAsync(List<ComponentGlobalReviewDto> reviews);

        Task<PageResponse<ComponentLocalReviewDto>?> GetComponentLocalReviewsPageAsync(
            ComponentLocalReviewFilter filter, Guid componentId);
        Task<PageResponse<ComponentGlobalReviewDto>?> GetComponentGlobalReviewsPageAsync(
            ComponentGlobalReviewFilter filter, Guid componentId);
        Task<ReviewsMetricResponse> GetReviewsMetricForComponent(Guid componentId);
    }
}
