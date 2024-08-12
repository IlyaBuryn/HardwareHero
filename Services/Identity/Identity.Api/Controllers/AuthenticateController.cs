using EventStream.EventHandling;
using Identity.Api.Contracts;
using KafkaEventStream.BackgroundServices;
using static Identity.Api.Records.RequestModels;

namespace Identity.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/identity/account")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageConsumer _messageConsumer;

        public AuthenticateController(
            IAuthService authService,
            IConfiguration configuration,
            IMessageProducer messageProducer,
            IMessageConsumer messageConsumer)
        {
            _authService = authService;
            _configuration = configuration;
            _messageProducer = messageProducer;
            _messageConsumer = messageConsumer;
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpRequestModel model)
        {
            var user = await _authService.SignUpAsync(model);
            var result = await _authService.GenerateJwtTokenAsync(user);

            if (result.IsSuccessful)
            {
                await RequestService.CallAndWaitServiceAsync(
                    new MailTopics(), $"mail/welcome/{user.Email}", _messageProducer, _messageConsumer);
            }
            
            AppendCookies(result);

            return Created(nameof(SignUpUserAsync), result);
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInUserAsync([FromBody] SignInRequestModel model)
        {
            var user = await _authService.SignInAsync(model);
            var result = await _authService.GenerateJwtTokenAsync(user);

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

        private void AppendCookies(AuthenticationResponse model)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            };

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
