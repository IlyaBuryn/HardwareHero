using Identity.Api.Contracts;
using Identity.Shared.Requests;
using Identity.Shared.Responses;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/identity/account")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            IAuthService authService,
            IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpRequest model)
        {
            var user = await _authService.SignUpAsync(model);
            var result = await _authService.GenerateJwtTokenAsync(user);
            result.UserId = user.Id;

            // TODO: change later
            //if (result.IsSuccessful)
            //{
            //    await RequestService.CallAndWaitServiceAsync(
            //        new MailTopics(), $"mail/welcome/{user.Email}", _messageProducer, _messageConsumer);
            //}
            
            AppendCookies(result, model.StayIn);

            return Created(nameof(SignUpUserAsync), result);
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInUserAsync([FromBody] SignInRequest model)
        {
            var user = await _authService.SignInAsync(model);
            var result = await _authService.GenerateJwtTokenAsync(user);
            result.UserId = user.Id;

            AppendCookies(result, model.StayIn);

            return Ok(result);
        }

        [HttpPut("password")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UserPasswordChangeRequest model)
        {
            var result = await _authService.UpdatePasswordAsync(model);

            AppendCookies(result);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var result = await _authService.RefreshTokenAsync(tokenRequest);

            AppendCookies(result);

            return Ok(result);
        }

        private void AppendCookies(AuthenticationResponse model, bool userStayIn = true)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax, // TODO:
                Domain = "localhost", // TODO:
                Path = "/", // TODO:
            };

            if (userStayIn)
            {
                cookieOptions.Expires = DateTime.Now + TimeSpan.FromDays(30);
            }

            var jwtCookieKey = _configuration.GetSection("JwtConfig:JwtCookieKey").Value;
            var refreshCookieKey = _configuration.GetSection("JwtConfig:RefreshCookieKey").Value;

            if (jwtCookieKey.IsNullOrEmpty())
            {
                throw new NullReferenceException(nameof(jwtCookieKey));
            }

            if (refreshCookieKey.IsNullOrEmpty())
            {
                throw new NullReferenceException(nameof(refreshCookieKey));
            }

            Response.Cookies.Append(jwtCookieKey!, model.AccessToken, cookieOptions);
            Response.Cookies.Append(refreshCookieKey!, model.RefreshToken, cookieOptions);
        }
    }
}
