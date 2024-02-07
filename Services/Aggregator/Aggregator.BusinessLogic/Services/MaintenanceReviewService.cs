namespace Aggregator.BusinessLogic.Services
{
    public class MaintenanceReviewService : IMaintenanceReviewService
    {
        public Task<Guid?> AddGlobalReviewAsync(MaintenanceGlobalReviewDto reviewToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<Guid[]> AddGlobalReviewsFromJsonAsync(string jsonData)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> AddLocalReviewAsync(MaintenanceLocalReviewDto reviewToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<List<ComponentGlobalReviewDto?>> GetComponentGlobalReviewsAsPageByMaintenanceIdAsync(PaginationInfo paginationInfo, Guid maintenanceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ComponentLocalReviewDto?>> GetComponentLocalReviewsAsPageByMaintenanceIdAsync(PaginationInfo paginationInfo, Guid maintenanceId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetMaintenanceAvgMarkAsync(Guid maintenanceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveGlobalReviewAsync(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLocalReviewAsync(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGlobalReviewAsync(MaintenanceGlobalReviewDto reviewToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLocalReviewAsync(MaintenanceLocalReviewDto reviewToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
