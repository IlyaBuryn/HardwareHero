namespace HardwareHero.Shared.DTOs.Configurator
{
    public class CustomAssemblyDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string AssemblyCategory { get; set; }
        public IEnumerable<Guid> ComponentIds { get; set; }
    }
}
