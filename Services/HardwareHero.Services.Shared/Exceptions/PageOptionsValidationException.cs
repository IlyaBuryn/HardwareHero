namespace HardwareHero.Services.Shared.Exceptions
{
    [Serializable]
    public class PageOptionsValidationException : Exception
    {
        public PageOptionsValidationException()
            : base("Incorrect page number and(or) size provided!")
        { }
    }
}
