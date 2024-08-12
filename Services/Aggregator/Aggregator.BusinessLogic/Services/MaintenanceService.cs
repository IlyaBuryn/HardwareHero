namespace Aggregator.BusinessLogic.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        public Task<Guid?> AddMaintenanceAsync(MaintenanceDto maintenanceToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<Guid[]> AddMaintenancesFromJsonAsync(string jsonData)
        {
            throw new NotImplementedException();
        }

        public Task<MaintenanceDto?> GetMaintenanceByIdAsync(Guid maintenanceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MaintenanceDto?>> GetMaintenancesAsPageAsync(FilterRequestDomain<Maintenance> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveMaintenanceAsync(Guid maintenanceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMaintenanceAsync(MaintenanceDto maintenanceToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
