﻿using Aggregator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;

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

        public Task<List<MaintenanceDto?>> GetMaintenancesAsPageAsync(Filter<Maintenance> filter)
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
