namespace HardwareHero.Services.Shared.Exceptions
{
    [Serializable]
    public class AlreadyExistException<T> : Exception
    {
        public AlreadyExistException()
            : base($"This {nameof(T)} is already exist!")
        { }

        public AlreadyExistException(string entity)
            : base($"This {nameof(T)}: \\\"{entity}\\\" is already exist!")
        { }
    }
}
