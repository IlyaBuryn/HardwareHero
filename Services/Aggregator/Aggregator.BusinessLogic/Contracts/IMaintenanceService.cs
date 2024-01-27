using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IMaintenanceService
    {
        Task<Guid?> AddMaintenanceAsync(MaintenanceDto maintenanceToAdd);
        Task<bool> UpdateMaintenanceAsync(MaintenanceDto maintenanceToUpdate);
        Task<bool> RemoveMaintenanceAsync(Guid maintenanceId);

        Task<Guid[]> AddMaintenancesFromJsonAsync(string jsonData);

        Task<MaintenanceDto?> GetMaintenanceByIdAsync(Guid maintenanceId);
        Task<List<MaintenanceDto?>> GetMaintenancesAsPageAsync(Filter<Maintenance> filter);
    }
}
