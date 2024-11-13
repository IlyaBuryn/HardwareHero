namespace HardwareHero.Shared.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("Entity doesn't exist!")
        { }

        public NotFoundException(string entity)
            : base($"This entity doesn't exist: {entity}!")
        { }
    }
}
