using Configurator.BusinessLogic.Models;
using Configurator.DTOs.Domain;

namespace Configurator.BusinessLogic.Contracts
{
    public interface IAssemblyService
    {
        Task<List<StoredAssemblyDto?>> GetAssembliesByUserIdAsync(Guid userId);
        void AddComponentAsync(Guid assemblyId, ConfiguratorComponent componentToAdd);
        void RemoveComponentAsync(Guid assemblyId, Guid componentId);
        Task<Guid?> SaveAssemblyAsync(StoredAssemblyDto assemblyToAdd);
        Task<bool> UpdateAssemblyAsync(StoredAssemblyDto assemblyToUpdate);
        Task<bool> RemoveAssemblyAsync(Guid assemblyId);
    }
}
