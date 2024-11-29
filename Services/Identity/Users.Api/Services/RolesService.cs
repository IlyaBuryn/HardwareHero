using EventDriven.Shared.Services;
using Identity.Shared.Domain;
using Users.Api.Contracts;
using static Identity.Shared.Requests.UsersRequestRecords;
using static Identity.Shared.Responses.UsersResponseRecords;

namespace Users.Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RolesService> _logger;

        public RolesService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<RolesService> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<RolesResponse> CreateRolesAsync(RolesRequest rolesToCreate)
        {
            ArgumentNullException.ThrowIfNull(rolesToCreate, nameof(rolesToCreate));
            
            var result = new Dictionary<string, string?>();
            var roles = rolesToCreate.Roles.Distinct();
            foreach (var role in roles)
            {
                var exist = await _roleManager.RoleExistsAsync(role);
                if (exist)
                {
                    result.Add(role, 
                        new AlreadyExistException(nameof(role), typeof(IdentityRole)).Message);
                }
                else
                {
                    var identityRole = new IdentityRole(role);
                    var creationResult = await _roleManager.CreateAsync(identityRole);

                    result.Add(
                        role, 
                        creationResult.Succeeded ? 
                            null : 
                            throw new Exception(creationResult.Errors.First().Description));
                }
            }

            _logger.LogInformation($"Roles has been created: {string.Join(',', roles)}");

            return new RolesResponse(result);
        }


        public async Task<RolesResponse> DeleteRolesAsync(RolesRequest rolesToDelete)
        {
            ArgumentNullException.ThrowIfNull(rolesToDelete, nameof(rolesToDelete));

            var result = new Dictionary<string, string?>();
            var roles = rolesToDelete.Roles.Distinct();
            foreach (var role in roles)
            {
                var exist = await _roleManager.RoleExistsAsync(role);
                if (!exist)
                {
                    result.Add(role,
                        new NotFoundException(nameof(role)).Message);
                }
                else
                {
                    var identityRole = new IdentityRole(role);
                    var deleteResult = await _roleManager.DeleteAsync(identityRole);

                    result.Add(
                        role,
                        deleteResult.Succeeded ?
                            null :
                            throw new Exception(deleteResult.Errors.First().Description));
                }
            }

            _logger.LogInformation($"Roles has been removed: {string.Join(',', roles)}");

            return new RolesResponse(result);
        }


        public async Task<IEnumerable<IdentityRole>> FetchRolesAsync()
        {
            var result = await _roleManager.Roles.ToListAsync();

            return result;
        }


        public async Task<bool> ChangeRoleNameAsync(string fromRole, string toRole)
        {
            ArgumentNullException.ThrowIfNull(fromRole, nameof(fromRole));
            ArgumentNullException.ThrowIfNull(toRole, nameof(toRole));

            var identityRole = await _roleManager.FindByNameAsync(fromRole);
            if (identityRole == null)
            {
                throw new NotFoundException(nameof(fromRole));
            }

            var toRoleExist = await _roleManager.RoleExistsAsync(toRole);
            if (toRoleExist)
            {
                throw new AlreadyExistException(nameof(toRole), typeof(IdentityRole));
            }

            identityRole.Name = toRole;
            var updateResult = await _roleManager.UpdateAsync(identityRole);

            if (updateResult.Succeeded)
            {
                _logger.LogInformation($"Role has been changed: {fromRole} -> {toRole}");

                return true;
            }

            throw new Exception(updateResult.Errors.First().Description);
        }


        public async Task<bool> SetupUserRolesAsync(UserRolesRequest userRoles)
        {
            ArgumentNullException.ThrowIfNull(userRoles, nameof(userRoles));

            var identityUser = await _userManager.FindByIdAsync(userRoles.UserId);
            if (identityUser == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var roles = userRoles.Roles.Distinct();
            var result = await _userManager.AddToRolesAsync(identityUser, roles);

            if (result.Succeeded)
            {
                _logger.LogInformation(
                    $"New roles are assigned to user: {identityUser.UserName} [ {string.Join(',', roles)} ]");

                return true;
            }

             throw new Exception(result.Errors.First().Description);
        }


        public async Task<bool> RemoveUserRolesAsync(UserRolesRequest userRoles)
        {
            ArgumentNullException.ThrowIfNull(userRoles, nameof(userRoles));

            var identityUser = await _userManager.FindByIdAsync(userRoles.UserId);
            if (identityUser == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var roles = userRoles.Roles.Distinct();
            var result = await _userManager.RemoveFromRolesAsync(identityUser, roles);

            if (result.Succeeded )
            {
                _logger.LogInformation(
                    $"The roles of the user have been removed: " +
                    $"{identityUser.UserName} [ {string.Join(',', roles)} ]");

                return true;
            }

            throw new Exception(result.Errors.First().Description);
        }
    }
}
