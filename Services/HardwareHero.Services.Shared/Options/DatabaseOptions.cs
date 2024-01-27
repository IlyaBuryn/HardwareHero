namespace HardwareHero.Services.Shared.Options
{
    public class DatabaseOptions
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public Dictionary<string, CollectionOptions>? Collections { get; set; }

    }
}
