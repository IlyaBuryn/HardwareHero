using Users.Api.Contracts;
using Users.Api.Filters;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Users.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest model)
        {
            var result = await _userService.CreateUserAsync(model);

            return CreatedAtAction(nameof(CreateUserAsync), result);
        }


        [HttpPut]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest model)
        {
            var result = await _userService.UpdateUserAsync(model);

            return Ok(result);
        }


        [HttpDelete("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> RemoveUserAsync([FromRoute] string userId)
        {
            var result = await _userService.RemoveUserAsync(userId);

            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> FetchUsersAsync([FromQuery] UsersFilter filter)
        {
            var result = await _userService.FetchUsersAsync(filter);

            return Ok(result);
        }


        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);

            return Ok(result);
        }
    }
}
