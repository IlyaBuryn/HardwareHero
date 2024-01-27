namespace HardwareHero.Services.Shared.Options
{
    // TODO: ?
    public class ServiceAddressOptions
    {
        public const string SectionName = nameof(ServiceAddressOptions);
        public string? IdentityServer { get; set; }
        public string? UserManagementService { get; set; }
    }
}
