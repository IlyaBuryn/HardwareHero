namespace HardwareHero.Services.Shared.Exceptions
{
    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string entity)
            : base($"This {entity} is already exist!")
        { }

        public AlreadyExistException(string entityType, string entity)
            : base($"This {entityType}: \\\"{entity}\\\" is already exist!")
        { }

        public AlreadyExistException(string entityType, string entity, string state)
            : base($"This {entityType} {entity} is already {state}!")
        { }
    }
}
