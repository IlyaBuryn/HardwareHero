using EventDriven.Shared.Services;
using Gateway.Ocelot.Extensions;
using HardwareHero.Shared.Exceptions;
using Identity.Domain.Messages.Tokens;
using Identity.Domain.Messages.Users;
using System.IdentityModel.Tokens.Jwt;

namespace Gateway.Ocelot.Middlewares
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IRequestService<TokenRequestMessage,
            AuthenticationResponseMessage> _refreshTokenService;
        private readonly List<string> _excludedRoutes;

        public RefreshTokenMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            IRequestService<TokenRequestMessage,
                AuthenticationResponseMessage> refreshTokenService)
        {
            _next = next;
            _configuration = configuration;
            _refreshTokenService = refreshTokenService;
            _excludedRoutes = configuration.GetSection("ExcludedRoutes")
                .Get<List<string>>() ?? new List<string>();
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";
            if (_excludedRoutes.Any(route => path.StartsWith(route, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);

                return;
            }

            var accessTokenKey = _configuration.GetSection("JwtConfig:JwtCookieKey").Value ?? string.Empty;
            var refreshTokenKey = _configuration.GetSection("JwtConfig:RefreshCookieKey").Value ?? string.Empty;

            var accessToken = string.Empty;
            var refreshToken = string.Empty;

            accessToken = context.Request.Cookies[accessTokenKey]?.Replace("Bearer ", "");
            refreshToken = context.Request.Cookies[refreshTokenKey];

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                await _next(context);

                return;
            }

            if (IsTokenNotExpired(accessToken))
            {
                var result = await TryCheckIfTokenExpiredAsync(accessToken, refreshToken);
                context.AppendAccessTokenToCookie(result.Value.Item1, accessTokenKey);
                context.AppendRefreshTokenToCookie(result.Value.Item2, refreshTokenKey);
            }

            await _next(context);
        }


        private async Task<(string, string)?> TryCheckIfTokenExpiredAsync(
            string accessToken, string refreshToken)
        {
            var response = await _refreshTokenService.SendRequestAsync(
                new TokenRequestMessage()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                });

            if (response == null)
            {
                throw new DataValidationException("No response from Identity server!");
            }

            if (!response.IsSuccess)
            {
                throw new AuthenticationException(response.AnyError());
            }

            return (accessToken, refreshToken);
        }


        private bool IsTokenNotExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}
