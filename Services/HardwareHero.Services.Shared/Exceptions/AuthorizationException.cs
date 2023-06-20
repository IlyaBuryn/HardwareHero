namespace HardwareHero.Services.Shared.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) 
            : base(message)
        { }
    }
}
