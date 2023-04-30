namespace HardwareHero.Services.Shared.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, CollectionOptions> Collections { get; set; }

    }
}
