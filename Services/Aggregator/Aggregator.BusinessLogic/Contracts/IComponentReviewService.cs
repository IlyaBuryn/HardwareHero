using HardwareHero.Services.Shared.DTOs;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentReviewService
    {
        Task<List<ComponentReviewDto?>> GetComponentReviewsAsPageByComponentIdAsync(int pageNumber, int pageSize, Guid componentId);
        Task<Guid> AddComponentReviewAsync(ComponentReviewDto componentReviewToAdd);
    }
}
