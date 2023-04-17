namespace HardwareHero.Services.Shared.Options
{
    public class ServiceAddressOptions
    {
        public const string SectionName = nameof(ServiceAddressOptions);
        public string IdentityServer { get; set; }
        public string UserManagementService { get; set; }
    }
}
