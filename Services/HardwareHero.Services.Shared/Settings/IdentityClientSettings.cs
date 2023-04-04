namespace HardwareHero.Services.Shared.Settings
{
    public static class IdentityClientSettings
    {
        public const string AggregatorApiScope = "HardwareHero.Aggregator.Api";
        public const string UsersApiScope = "HardwareHero.Users.Api";
        public const string WebScope = "HardwareHero.Web";

        public const string GrantType_ClientCredentials = "client_credentials";
        public const string GrantType_Password = "password";
    }
}
