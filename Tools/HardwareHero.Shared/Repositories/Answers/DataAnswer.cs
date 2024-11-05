namespace HardwareHero.Shared.Repositories.Answers
{
    public class DataAnswer<T> : Answer<T> where T : class
    {
        public T? Value { get; set; }

        public DataAnswer(T? value) : base()
        {
            Value = value;
        }

        public DataAnswer(Exception exception) : base(exception) { }
    }
}
