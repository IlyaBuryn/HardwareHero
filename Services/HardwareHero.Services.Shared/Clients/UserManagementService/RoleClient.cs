using HardwareHero.Services.Shared.Clients.Contracts;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HardwareHero.Services.Shared.Clients.UserManagementService
{
    public class RoleClient : UserManagementBaseClient, IRoleClient
    {
        public RoleClient(HttpClient httpClient, IOptions<ServiceAddressOptions> options) : base(httpClient, options) { }

        public async Task<IdentityResult> AddAsync(IdentityRole role)
            => await SendPostRequest(role, $"/gateway/role");

        public async Task<UserManagementServiceResponse<IdentityRole>> GetAsync(string name)
            => await SendGetRequest<IdentityRole>($"/gateway/role/{name}");

        public async Task<UserManagementServiceResponse<IEnumerable<IdentityRole>>> GetAllAsync()
            => await SendGetRequest<IEnumerable<IdentityRole>>($"/gateway/role");

        public async Task<IdentityResult> RemoveAsync(string property)
            => await SendDeleteRequest(property, $"/gateway/role/remove");

        public async Task<IdentityResult> UpdateAsync(IdentityRole role)
            => await SendPutRequest(role, $"/gateway/role");
    }
}
