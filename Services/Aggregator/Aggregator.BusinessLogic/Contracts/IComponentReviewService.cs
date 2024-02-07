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

        Task<ComplexResponse> AddGlobalReviewsAsync(List<ComponentGlobalReviewDto> reviews);

        Task<PageResponse<ComponentLocalReviewDto?>> GetComponentLocalReviewsAsPageByComponentIdAsync(
            ComponentLocalReviewFilter filter, Guid componentId);
        Task<PageResponse<ComponentGlobalReviewDto?>> GetComponentGlobalReviewsAsPageByComponentIdAsync(
            ComponentGlobalReviewFilter filter, Guid componentId);
        Task<AvgReviewsMarksResponse> GetComponentAvgMarkAsync(Guid componentId);
    }
}
