namespace Aggregator.BusinessLogic.Contracts
{
    public interface IMaintenanceTypeService
    {
        Task<Guid?> AddMaintenanceTypeAsync(MaintenanceTypeDto maintenanceTypeToAdd);
        Task<bool> UpdateMaintenanceTypeAsync(MaintenanceTypeDto maintenanceTypeToUpdate);
        Task<bool> RemoveMaintenanceTypeAsync(MaintenanceTypeDto maintenanceTypeToRemove);

        Task<List<MaintenanceTypeDto?>> GetMaintenanceTypesAsync();
    }
}
