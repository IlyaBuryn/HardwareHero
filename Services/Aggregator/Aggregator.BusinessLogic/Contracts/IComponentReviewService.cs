using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Responses;

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

        Task<Guid[]> AddGlobalReviewsFromJsonAsync(string jsonData);

        Task<PageResponse<ComponentLocalReviewDto?>> GetComponentLocalReviewsAsPageByComponentIdAsync(PaginationInfo paginationInfo, Guid componentId);
        Task<PageResponse<ComponentGlobalReviewDto?>> GetComponentGlobalReviewsAsPageByComponentIdAsync(PaginationInfo paginationInfo, Guid componentId);
        Task<AvgReviewsMarksResponse> GetComponentAvgMarkAsync(Guid componentId);
    }
}
