namespace HardwareHero.Shared.Exceptions
{
    [Serializable]
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base($"Wrong username or password!")
        { }

        public AuthenticationException(string message)
            : base(message)
        { }
    }
}
