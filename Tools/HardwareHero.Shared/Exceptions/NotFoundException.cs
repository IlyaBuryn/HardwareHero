namespace HardwareHero.Shared.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("Object doesn't exist!")
        { }

        public NotFoundException(string message)
            : base($"This entity doesn't exist: {message}!")
        { }
    }
}
