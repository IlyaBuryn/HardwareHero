using Identity.Api.Contracts;

namespace Identity.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/identity/claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsService _claimsService;

        public ClaimsController(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserClaimsAsync([FromRoute] string userId)
        {
            var result = await _claimsService.GetClaimsAsync(userId);

            return Ok(result);
        }

        [HttpPost("{userId}/{claimName}/{claimValue}")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserClaimAsync(
            [FromRoute] string userId, [FromRoute] string claimName, [FromRoute] string claimValue)
        {
            var result = await _claimsService.AddClaimsAsync(userId, claimName, claimValue);

            return Ok(result);
        }
    }
}
