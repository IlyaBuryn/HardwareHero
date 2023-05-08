using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/role")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpPost]
        public async Task<IdentityResult> AddAsync(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }


        [HttpPut]
        public async Task<IdentityResult> UpdateAsync(IdentityRole role)
        {
            var roleToBeUpdated = await _roleManager.FindByIdAsync(role.Id);
            if (roleToBeUpdated == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"Role {role.Name} was not found!" });
            }

            roleToBeUpdated.Name = role.Name;

            var result = await _roleManager.UpdateAsync(roleToBeUpdated);
            return result;
        }

        [HttpDelete("remove/{roleName}")]
        public async Task<IdentityResult> RemoveAsync([FromRoute] string roleName)
        {
            var roleToDelete = await _roleManager.FindByNameAsync(roleName);
            if (roleToDelete == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"Role {roleName} was not found!" });
            }

            var result = await _roleManager.DeleteAsync(roleToDelete);
            return result;
        }

        [HttpGet("{name}")]
        public Task<IdentityRole> GetAsync([FromRoute] string name)
        {
            var result = _roleManager.FindByNameAsync(name);
            return result;
        }

        [HttpGet]
        public IEnumerable<IdentityRole> GetAllAsync()
        {
            var result = _roleManager.Roles.AsEnumerable();
            return result;
        }
    }
}
