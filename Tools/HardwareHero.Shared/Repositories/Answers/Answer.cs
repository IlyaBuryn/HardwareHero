namespace HardwareHero.Shared.Repositories.Answers
{
    public class Answer<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public Exception[] Errors { get; set; } = Array.Empty<Exception>();

        public Answer()
        {
            IsSuccess = true;
        }

        public Answer(params Exception[] exceptions)
        {
            IsSuccess = false;
            Errors = exceptions;
        }
    }
}
