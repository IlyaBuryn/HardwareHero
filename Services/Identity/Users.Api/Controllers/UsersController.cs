using Users.Api.Contracts;
using static Users.Api.Records.RequestModels;

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
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestModel model)
        {
            var result = await _userService.CreateUserAsync(model);

            return CreatedAtAction(nameof(CreateUserAsync), result);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, UpdateUserRequestModel model)
        {
            var result = await _userService.UpdateUserAsync(userId, model);

            return Ok(result);
        }


        [HttpDelete("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> RemoveUserAsync([FromRoute] string userId)
        {
            var result = await _userService.RemoveUserAsync(userId);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();

            return Ok(result);
        }
    }
}
