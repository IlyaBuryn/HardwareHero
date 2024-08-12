using HardwareHero.Filter.Operations;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IMaintenanceReviewService
    {
        Task<Guid?> AddLocalReviewAsync(MaintenanceLocalReviewDto reviewToAdd);
        Task<bool> UpdateLocalReviewAsync(MaintenanceLocalReviewDto reviewToAdd);
        Task<bool> RemoveLocalReviewAsync(Guid reviewId);

        Task<Guid?> AddGlobalReviewAsync(MaintenanceGlobalReviewDto reviewToAdd);
        Task<bool> UpdateGlobalReviewAsync(MaintenanceGlobalReviewDto reviewToAdd);
        Task<bool> RemoveGlobalReviewAsync(Guid reviewId);

        Task<Guid[]> AddGlobalReviewsFromJsonAsync(string jsonData);

        Task<List<ComponentLocalReviewDto?>> GetComponentLocalReviewsAsPageByMaintenanceIdAsync(IPaginable filter, Guid maintenanceId);
        Task<List<ComponentGlobalReviewDto?>> GetComponentGlobalReviewsAsPageByMaintenanceIdAsync(IPaginable filter, Guid maintenanceId);
        Task<decimal> GetMaintenanceAvgMarkAsync(Guid maintenanceId);
    }
}
