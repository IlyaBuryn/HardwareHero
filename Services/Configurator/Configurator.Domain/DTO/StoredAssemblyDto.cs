namespace Configurator.Domain.DTO
{
    public class StoredAssemblyDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public ICollection<AssemblyComponentDto>? SelectedComponents { get; set; }
            = new List<AssemblyComponentDto>();
    }
}
