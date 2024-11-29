using Users.Api.Contracts;
using static Identity.Shared.Requests.UsersRequestRecords;

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


        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateRolesAsync([FromQuery] RolesRequest rolesToCreate)
        {
            var result = await _rolesService.CreateRolesAsync(rolesToCreate);

            return CreatedAtAction(nameof(CreateRolesAsync), result);
        }


        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> RemoveRolesAsync([FromQuery] RolesRequest rolesToDelete)
        {
            var result = await _rolesService.DeleteRolesAsync(rolesToDelete);

            return Ok(result);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await _rolesService.FetchRolesAsync();

            return Ok(result);
        }


        [HttpPut("{fromRole}/{toRole}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute] string fromRole, [FromRoute] string toRole)
        {
            var result = await _rolesService.ChangeRoleNameAsync(fromRole, toRole);

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> SetupUserRolesAsync([FromQuery] UserRolesRequest userRoles)
        {
            var result = await _rolesService.SetupUserRolesAsync(userRoles);

            return Ok(result);
        }


        [HttpDelete]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> RemoveUserRolesAsync([FromQuery] UserRolesRequest userRoles)
        {
            var result = await _rolesService.RemoveUserRolesAsync(userRoles);

            return Ok(result);
        }
    }
}
