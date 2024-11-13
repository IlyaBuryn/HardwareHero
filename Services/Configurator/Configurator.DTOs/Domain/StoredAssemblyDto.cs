namespace Configurator.DTOs.Domain
{
    public class StoredAssemblyDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public List<ConfiguratorComponentDto> SelectedComponents { get; set; }
    }
}
