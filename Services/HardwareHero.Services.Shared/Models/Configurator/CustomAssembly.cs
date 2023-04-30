using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Services.Shared.Models.Configurator
{
    public class CustomAssembly
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("User")]
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        [BsonElement("Category")]
        public string AssemblyCategory { get; set; }
        public IEnumerable<Guid> ComponentIds { get; set; }
    }
}
