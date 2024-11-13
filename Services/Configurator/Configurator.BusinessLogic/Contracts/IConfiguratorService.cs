using Configurator.BusinessLogic.Models;
using Configurator.DTOs.Domain;

namespace Configurator.BusinessLogic.Contracts
{
    public interface IConfiguratorService
    {
        Task<AssemblyCompatibilityResult> CheckCompatibility(StoredAssembly assembly);
    }
}
