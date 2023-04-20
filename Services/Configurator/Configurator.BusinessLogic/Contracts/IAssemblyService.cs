﻿using HardwareHero.Services.Shared.DTOs.Configurator;

namespace Configurator.BusinessLogic.Contracts
{
    public interface IAssemblyService
    {
        Task<List<CustomAssemblyDto?>> GetAssemblyListAsync();
        Task<List<CustomAssemblyDto?>> GetAssemblyListByUserIdAsync(Guid userId, string category);
        Task<Guid?> AddAssemblyAsync(CustomAssemblyDto assemblyToAdd);
        Task<bool> UpdateAssemblyAsync(CustomAssemblyDto assemblyToUpdate);
        Task<bool> RemoveAssemblyAsync(Guid assemblyId);
        Task<List<Guid>?> GetComponentIdsByAssemblyIdAsync(Guid assemblyId);

    }
}
