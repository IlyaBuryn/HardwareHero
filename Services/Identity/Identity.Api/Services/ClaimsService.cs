using Identity.Api.Contracts;

namespace Identity.Api.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<ClaimsService> _logger;

        public ClaimsService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<ClaimsService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<bool> AddClaimsAsync(string userId, string claimName, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userClaim = new Claim(claimName, claimValue);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var result = await _userManager.AddClaimAsync(user, userClaim);
            if (!result.Succeeded)
            {
                throw new AuthenticationProblemException(result.Errors.First().Description);
            }

            return result.Succeeded;
        }

        public async Task<IList<Claim>> GetClaimsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var claims = await _userManager.GetClaimsAsync(user);

            return claims;
        }
    }
}
