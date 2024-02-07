using HardwareHero.Shared.Constants;

namespace HardwareHero.Shared.Options
{
    public class IdentityServerWebAppOptions
    {
        public static string ClientId { get; set; } = "react.web.app";
        public static string ClientSecret { get; set; } = "secret";
        public static string Address { get; set; } = "http://identityserver/connect/token";
        public static string Scope { get; set; } = IdentityClientConstants.WebScope + ' ' + IdentityClientConstants.ServicesApiScope;
    }
}
