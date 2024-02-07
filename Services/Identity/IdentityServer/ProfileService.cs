using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using HardwareHero.Shared.Models.Users;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim> { };
            claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null) && user.LockoutEnabled;
        }
    }
}
