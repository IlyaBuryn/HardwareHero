using static Identity.Shared.Requests.UsersRequestRecords;
using static Identity.Shared.Responses.UsersResponseRecords;

namespace Users.Api.Contracts
{
    public interface IRolesService
    {
        Task<RolesResponse> CreateRolesAsync(RolesRequest rolesToCreate);
        Task<RolesResponse> DeleteRolesAsync(RolesRequest rolesToDelete);
        Task<IEnumerable<IdentityRole>> FetchRolesAsync();

        Task<bool> ChangeRoleNameAsync(string fromRole, string toRole);

        Task<bool> SetupUserRolesAsync(UserRolesRequest userRoles);
        Task<bool> RemoveUserRolesAsync(UserRolesRequest userRoles);
    }
}
