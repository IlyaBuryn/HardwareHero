using HardwareHero.Services.Shared.Models.UserManagementService;
using HardwareHero.Services.Shared.Requests;
using HardwareHero.Services.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Services.Shared.Clients.Contracts
{
    public interface IUserClient
    {
        Task<IdentityResult> AddAsync(CreateUserRequest request);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);

        Task<IdentityResult> RemoveAsync(string property);

        Task<UserManagementServiceResponse<ApplicationUser>> GetAsync(string name);

        Task<UserManagementServiceResponse<IEnumerable<ApplicationUser>>> GetAllAsync();

        Task<IdentityResult> ChangePasswordAsync(UserPasswordChangeRequest request);

        Task<IdentityResult> AddToRoleAsync(AddRemoveRoleRequest request);

        Task<IdentityResult> AddToRolesAsync(AddRemoveRolesRequest request);

        Task<IdentityResult> RemoveFromRoleAsync(AddRemoveRoleRequest request);

        Task<IdentityResult> RemoveFromRolesAsync(AddRemoveRolesRequest request);
    }
}
