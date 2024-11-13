using EventStream.EventHandling;
using HardwareHero.Shared.Exceptions;
using HardwareHero.Shared.Responses;
using Identity.Shared.Responses;
using KafkaEventStream.BackgroundServices;
using KafkaEventStream.Topics;
using Ocelot.Middleware;
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
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwtCookieKey = _configuration.GetSection("JwtConfig:JwtCookieKey").Value ?? string.Empty;
            var refreshCookieKey = _configuration.GetSection("JwtConfig:RefreshCookieKey").Value ?? string.Empty;

            // Get tokens from cookies
            var accessToken = string.Empty;
            var refreshToken = string.Empty;


            accessToken = context.Request.Cookies[jwtCookieKey]?.Replace("Bearer ", "");
            refreshToken = context.Request.Cookies[refreshCookieKey];
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken)) 
            {
                await _next(context);
                return;
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
                    HttpOnly = false,
                    Secure = false,
                    SameSite = SameSiteMode.Lax, // TODO:
                    Domain = "localhost", // TODO:
                    Path = "/", // TODO:
                };

                cookieOptions.Expires = DateTime.Now + TimeSpan.FromDays(30); // TODO:

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
