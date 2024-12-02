namespace Gateway.Ocelot.Extensions
{
    public static class HttpContextExtensions
    {
        public static HttpContext AppendAccessTokenToCookie(
            this HttpContext context, string accessToken, string accessTokenKey)
        {
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(accessTokenKey))
            {
                return context;
            }

            var cookieOptions = ConfigureCookieOption();

            context.Response.Cookies.Append(accessTokenKey, accessToken, cookieOptions);

            return context;
        }

        public static HttpContext AppendRefreshTokenToCookie(
            this HttpContext context, string refreshToken, string refreshTokenKey)
        {
            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(refreshTokenKey))
            {
                return context;
            }

            var cookieOptions = ConfigureCookieOption();

            context.Response.Cookies.Append(refreshTokenKey, refreshToken, cookieOptions);

            return context;
        }

        private static CookieOptions ConfigureCookieOption()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax, // TODO:
                Domain = "localhost", // TODO:
                Path = "/", // TODO:
            };

            cookieOptions.Expires = DateTime.Now + TimeSpan.FromDays(30); // TODO:

            return cookieOptions;
        }
    }
}
