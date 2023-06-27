namespace HardwareHero.Services.Shared.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, CollectionOptions> Collections { get; set; }

    }
}
