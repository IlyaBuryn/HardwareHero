namespace HardwareHero.Services.Shared.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        public DataValidationException()
            : base($"Incorrect validation data!")
        { }

        public DataValidationException(string message)
            : base($"Incorrect validation data for: \"{message}\"!")
        { }
    }
}
