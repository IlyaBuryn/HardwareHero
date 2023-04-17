using HardwareHero.Services.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Services.Shared.Clients.Contracts
{
    public interface IRoleClient
    {
        Task<IdentityResult> AddAsync(IdentityRole role);

        Task<UserManagementServiceResponse<IdentityRole>> GetAsync(string name);

        Task<UserManagementServiceResponse<IEnumerable<IdentityRole>>> GetAllAsync();

        Task<IdentityResult> RemoveAsync(string property);

        Task<IdentityResult> UpdateAsync(IdentityRole role);
    }
}
