using HardwareHero.Services.Shared.Constants;

namespace HardwareHero.Services.Shared.Options
{
    // TODO: ?
    public class IdentityServerApiOptions
    {
        public const string SectionName = nameof(IdentityServerApiOptions);
        public string ClientId { get; set; } = "test.client";
        public string ClientSecret { get; set; } = "511536EF-F270-4058-80CA-1C89C192F69A";
        public string Scope { get; set; } = IdentityClientConstants.ServicesApiScope;
        public string GrantType { get; set; } = IdentityClientConstants.GrantType_ClientCredentials;
    }
}
