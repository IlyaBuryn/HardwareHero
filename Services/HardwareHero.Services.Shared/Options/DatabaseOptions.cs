namespace HardwareHero.Services.Shared.Options
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, CollectionOptions> Collections { get; set; }

    }
}
