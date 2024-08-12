using Users.Api.Contracts;

namespace Users.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/roles")]
    [Authorize(Roles = Roles.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost("{roleName}")]
        public async Task<IActionResult> CreateRoleAsync([FromRoute] string roleName)
        {
            var result = await _rolesService.CreateRoleAsync(roleName);

            return CreatedAtAction(nameof(CreateRoleAsync), result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await _rolesService.GetAllRolesAsync();

            return Ok(result);
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> RemoveRoleAsync([FromRoute] string roleName)
        {
            var result = await _rolesService.RemoveRoleAsync(roleName);

            return Ok(result);
        }

        [HttpDelete("{roleName}/{userId}")]
        public async Task<IActionResult> RemoveUserRoleAsync([FromRoute] string roleName, [FromRoute] string userId)
        {
            var result = await _rolesService.RemoveUserRoleAsync(userId, roleName);

            return Ok(result);
        }

        [HttpPost("{userId}/{roleName}")]
        public async Task<IActionResult> SetupUserRoleAsync([FromRoute] string userId, [FromRoute] string roleName)
        {
            var result = await _rolesService.SetupUserRoleAsync(userId, roleName);

            return Ok(result);
        }

        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> SetupUserRolesAsync([FromRoute] string userId, [FromBody] string[] roles)
        {
            var result = await _rolesService.SetupUserRolesAsync(userId, roles);

            return Ok(result);
        }
    }
}
