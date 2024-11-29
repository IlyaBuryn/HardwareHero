using HardwareHero.Shared.Responses;
using Identity.Shared.Domain;
using Users.Api.Filters;
using static Identity.Shared.Requests.IdentityRequestRecords;
using static Identity.Shared.Responses.IdentityResponseRecords;

namespace Users.Api.Contracts
{
    public interface IUsersService
    {
        Task<ApplicationUser> CreateUserAsync(CreateUserRequest model);
        Task<ApplicationUser> UpdateUserAsync(UpdateUserRequest model);
        Task<bool> RemoveUserAsync(string userId);
        Task<PageResponse<UserResponse>> FetchUsersAsync(UsersFilter filter);
        Task<UserResponse> GetUserByIdAsync(string userId);
    }
}
