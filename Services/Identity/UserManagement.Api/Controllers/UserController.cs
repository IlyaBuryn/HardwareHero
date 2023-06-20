using HardwareHero.Services.Shared.Models.UserManagementService;
using HardwareHero.Services.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        public Task<IdentityResult> CreateAsync(CreateUserRequest request)
        {
            var result = _userManager.CreateAsync(request.User, request.Password);
            return result;
        }


        [HttpPut]
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var userToBeUpdated = await _userManager.FindByNameAsync(user.UserName);
            if (userToBeUpdated == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"User {user.UserName} was not found!" });
            }

            userToBeUpdated.WishList = user.WishList;
            userToBeUpdated.PhoneNumber = user.PhoneNumber;
            userToBeUpdated.Name = user.Name;
            userToBeUpdated.Email = user.Email;

            var result = await _userManager.UpdateAsync(userToBeUpdated);
            return result;
        }


        [HttpDelete("remove/{username}")]
        public async Task<IdentityResult> RemoveAsync([FromRoute] string username)
        {
            var userToDelete = await _userManager.FindByNameAsync(username);
            if (userToDelete == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"User {username} was not found!" });
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            return result;
        }


        [HttpGet("{name}")]
        public Task<ApplicationUser> GetAsync([FromRoute] string name)
        {
            var result = _userManager.FindByNameAsync(name);
            return result;
        }


        [HttpGet]
        public IEnumerable<ApplicationUser> GetAllAsync()
        {
            var result = _userManager.Users.AsEnumerable();
            return result;
        }


        [HttpPost("change-password")]
        public async Task<IdentityResult> ChangePasswordAsync(UserPasswordChangeRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"User {request.UserName} was not found!" });
            }

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            return result;
        }


        [HttpPost("add-to-role")]
        public async Task<IdentityResult> AddToRoleAsync(AddRemoveRoleRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"User {request.UserName} was not found!" });
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            return result;
        }


        [HttpPost("add-to-roles")]
        public async Task<IdentityResult> AddToRolesAsync(AddRemoveRolesRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError() { Description = $"User {request.UserName} was not found!" });

            var result = await _userManager.AddToRolesAsync(user, request.RoleNames);
            return result;
        }


        [HttpPost("remove-from-role")]
        public async Task<IdentityResult> RemoveFromRoleAsync(AddRemoveRoleRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError() { Description = $"User {request.UserName} was not found!" });

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            return result;
        }


        [HttpPost("remove-from-roles")]
        public async Task<IdentityResult> RemoveFromRolesAsync(AddRemoveRolesRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError() { Description = $"User {request.UserName} was not found!" });

            var result = await _userManager.RemoveFromRolesAsync(user, request.RoleNames);
            return result;
        }
    }
}
