namespace Aggregator.BusinessLogic.Contracts
{
    public interface IMaintenanceService
    {
        Task<Guid?> AddMaintenanceAsync(MaintenanceDto maintenanceToAdd);
        Task<bool> UpdateMaintenanceAsync(MaintenanceDto maintenanceToUpdate);
        Task<bool> RemoveMaintenanceAsync(Guid maintenanceId);

        Task<Guid[]> AddMaintenancesFromJsonAsync(string jsonData);

        Task<MaintenanceDto?> GetMaintenanceByIdAsync(Guid maintenanceId);
        Task<List<MaintenanceDto?>> GetMaintenancesAsPageAsync(FilterRequestDomain<Maintenance> filter);
    }
}
