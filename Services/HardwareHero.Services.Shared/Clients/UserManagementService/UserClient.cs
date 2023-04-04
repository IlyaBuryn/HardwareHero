using HardwareHero.Services.Shared.Clients.Contracts;
using HardwareHero.Services.Shared.Models.UserManagementService;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Requests;
using HardwareHero.Services.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HardwareHero.Services.Shared.Clients.UserManagementService
{
    public class UserClient : UserManagementBaseClient, IUserClient
    {
        public UserClient(HttpClient httpClient, IOptions<ServiceAddressOptions> options) : base(httpClient, options) { }

        public async Task<IdentityResult> AddAsync(CreateUserRequest request)
            => await SendPostRequest(request, $"/user/create");

        public async Task<IdentityResult> ChangePasswordAsync(UserPasswordChangeRequest request)
            => await SendPostRequest(request, $"/user/change-password");

        public async Task<IdentityResult> AddToRoleAsync(AddRemoveRoleRequest request)
            => await SendPostRequest(request, $"/user/add-to-role");

        public async Task<IdentityResult> AddToRolesAsync(AddRemoveRolesRequest request)
            => await SendPostRequest(request, $"/user/add-to-roles");

        public async Task<IdentityResult> RemoveFromRoleAsync(AddRemoveRoleRequest request)
            => await SendPostRequest(request, $"/user/remove-from-role");

        public async Task<IdentityResult> RemoveFromRolesAsync(AddRemoveRolesRequest request)
            => await SendPostRequest(request, $"/user/remove-from-roles");

        public async Task<UserManagementServiceResponse<ApplicationUser>> GetAsync(string name)
            => await SendGetRequest<ApplicationUser>($"user/{name}");

        public async Task<UserManagementServiceResponse<IEnumerable<ApplicationUser>>> GetAllAsync()
            => await SendGetRequest<IEnumerable<ApplicationUser>>($"/user/");

        public async Task<IdentityResult> RemoveAsync(string property)
            => await SendDeleteRequest(property, $"/user/remove/");

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
            => await SendPutRequest(user, $"/user/update");
    }
}
