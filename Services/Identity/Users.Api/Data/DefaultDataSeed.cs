using Identity.Shared.Domain;

namespace Users.Api.Data
{
    public class DefaultDataSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DefaultDataSeed> _logger;

        public DefaultDataSeed(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<DefaultDataSeed> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task EnsureSeedDataAsync()
        {
            foreach (var role in DefaultDataConfig.DefaultIdentityRoles)
            {
                var isRoleExist = await _roleManager.RoleExistsAsync(role.Name);
                if (isRoleExist)
                {
                    _logger.LogDebug($"This role \"{role.Name}\" already exist!");
                    continue;
                }

                var result = await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name, NormalizedName = role.NormalizedName });

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                _logger.LogDebug($"Role \"{role.Name}\" has been created!");
            }

            foreach (var user in DefaultDataConfig.DefaultIdentityUsers)
            {
                var existUserNameResult = await _userManager.FindByNameAsync(user.UserName);

                if (existUserNameResult != null)
                {
                    _logger.LogDebug($"This user \"{user.UserName}\" already exist!");
                    continue;
                }

                var existUserEmailResult = await _userManager.FindByEmailAsync(user.Email);

                if (existUserEmailResult != null)
                {
                    _logger.LogDebug($"This user \"{user.Email}\" already exist!");
                    continue;
                }

                var newUser = new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };

                var createdUserResult = await _userManager.CreateAsync(newUser, user.Password);
                if (!createdUserResult.Succeeded)
                {
                    throw new Exception(createdUserResult.Errors.First().Description);
                }

                _logger.LogDebug($"{user.UserName} has been created!");

                var applicationUser = await _userManager.FindByNameAsync(user.UserName);

                var createUserRoleResult = await _userManager.AddToRolesAsync(
                        applicationUser!, user.Roles);

                if (!createUserRoleResult.Succeeded)
                {
                    _logger.LogDebug($"Error, when add roles to user \"{user.UserName}\"!");
                    throw new Exception(createUserRoleResult.Errors.First().Description);
                }

                _logger.LogDebug($"Roles added to user \"{user.UserName}\"!");
            }
        }
    }
}
