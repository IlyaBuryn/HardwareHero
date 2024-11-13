using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;

namespace HardwareHero.Shared.Exceptions
{
    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException()
            : base("The entity is already exists!")
        { }

        public AlreadyExistException(BaseEntity? entity)
            : base($"The entity is already exist:\n" +
              entity == null ? "NULL" : $"{JsonSerializer.Serialize(
                  entity!,
                  new JsonSerializerOptions { WriteIndented = true })}")
        { }

        public AlreadyExistException(object? entity, Type entityType)
            : base($"The entity is already exist:\n" +
              entity == null ? "NULL" : $"{JsonSerializer.Serialize(
                  entity, 
                  entityType, 
                  new JsonSerializerOptions { WriteIndented = true })}")
        { }
    }
}
