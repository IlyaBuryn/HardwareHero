using Users.Api.Contracts;

namespace Users.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityRole> CreateRoleAsync(string roleName)
        {
            var isExist = await _roleManager.RoleExistsAsync(roleName);
            if (isExist)
            {
                throw new AlreadyExistException<IdentityRole>(nameof(roleName));
            }

            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded ? role : throw new Exception(result.Errors.First().Description);
        }

        public async Task<IList<IdentityRole>> GetAllRolesAsync()
        {
            var result = await _roleManager.Roles.ToListAsync();

            return result;
        }

        public async Task<bool> RemoveRoleAsync(string roleName)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExist)
            {
                throw new NotFoundException(nameof(roleName));
            }

            var result = await _roleManager.DeleteAsync(new IdentityRole(roleName));
            
            return result.Succeeded ? true : throw new Exception(result.Errors.First().Description);
        }

        public async Task<bool> RemoveUserRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(roleName));
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            return result.Succeeded ? true : throw new Exception(result.Errors.First().Description);
        }

        public async Task<bool> SetupUserRoleAsync(string userId, string roleName)
        {
            var userExist = await _userManager.FindByIdAsync(userId);
            if (userExist == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var result = await _userManager.AddToRoleAsync(userExist, roleName);

            return result.Succeeded ? true : throw new Exception(result.Errors.First().Description);
        }

        public async Task<bool> SetupUserRolesAsync(string userId, string[] roles)
        {
            var userExist = await _userManager.FindByIdAsync(userId);
            if (userExist == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var result = await _userManager.AddToRolesAsync(userExist, roles);

            return result.Succeeded ? true : throw new Exception(result.Errors.First().Description);
        }
    }
}
