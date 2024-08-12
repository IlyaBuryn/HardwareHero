using EventStream.EventHandling;
using HardwareHero.Shared.Exceptions;
using HardwareHero.Shared.Responses;
using KafkaEventStream.BackgroundServices;
using KafkaEventStream.Topics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ApiGateway.Ocelot.Middlewares
{
    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageConsumer _messageConsumer;
        private readonly List<(string PathTemplate, string HttpMethod)> _excludedRoutes;

        public TokenRefreshMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            IMessageProducer messageProducer,
            IMessageConsumer messageConsumer)
        {
            _next = next;
            _configuration = configuration;
            _messageProducer = messageProducer;
            _messageConsumer = messageConsumer;
            _excludedRoutes = new List<(string, string)>
            {
                ("/identity/account/sign-up", "POST"),
                ("/identity/account/sign-in", "POST"),
                ("/aggregator/components/page", "POST"),
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip if auth isn't required
            var requestPath = context.Request.Path.Value;
            var requestMethod = context.Request.Method.ToUpper();

            if (_excludedRoutes.Any(route =>
                string.Equals(route.PathTemplate, requestPath, StringComparison.OrdinalIgnoreCase)
                && string.Equals(route.HttpMethod, requestMethod, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            var jwtCookieKey = _configuration.GetSection("JwtConfig:JwtCookieKey").Value ?? string.Empty;
            var refreshCookieKey = _configuration.GetSection("JwtConfig:RefreshCookieKey").Value ?? string.Empty;

            // Get tokens from cookies
            var accessToken = string.Empty;
            var refreshToken = string.Empty;


            accessToken = context.Request.Cookies[jwtCookieKey]?.Replace("Bearer ", "");
            refreshToken = context.Request.Cookies[refreshCookieKey];
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken)) 
            { 
                throw new AuthenticationException();
            }

            // check if token is Expired
            if (IsTokenExpired(accessToken))
            {
                // Send message to Identity API
                var identityResponseJson = await RequestService.CallAndWaitServiceAsync(
                    new IdentityTopics(), $"refresh-token/{accessToken}/{refreshToken}", 
                    _messageProducer, _messageConsumer);

                var identityResponse = JsonSerializer.Deserialize<AuthenticationResponse>(identityResponseJson);

                if (identityResponse == null || !identityResponse.IsSuccessful)
                {
                    throw new AuthenticationException(identityResponse.Errors.First());
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                context.Response.Cookies.Append(jwtCookieKey, identityResponse.AccessToken, cookieOptions);
                context.Response.Cookies.Append(refreshCookieKey, identityResponse.RefreshToken, cookieOptions);
            }

            await _next(context);
        }

        private bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}
