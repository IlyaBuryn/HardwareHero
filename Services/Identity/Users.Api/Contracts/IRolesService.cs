namespace Users.Api.Contracts
{
    public interface IRolesService
    {
        Task<IdentityRole> CreateRoleAsync(string roleName);
        Task<IList<IdentityRole>> GetAllRolesAsync();
        Task<bool> SetupUserRoleAsync(string userId, string roleName);
        Task<bool> SetupUserRolesAsync(string userId, string[] roles);
        Task<bool> RemoveRoleAsync(string roleName);
        Task<bool> RemoveUserRoleAsync(string userId, string roleName);
    }
}
