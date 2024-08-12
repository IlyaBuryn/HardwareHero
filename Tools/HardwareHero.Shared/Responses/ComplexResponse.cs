namespace HardwareHero.Shared.Responses
{
    public class ComplexResponse
    {
        public ComplexResponse()
        {
            Responses = new List<TupleResponse>();
        }

        public List<TupleResponse> Responses { get; set; }

        public class TupleResponse
        {
            public TupleResponse(string? subject, string? result)
            {
                Subject = subject;
                Result = result;
            }

            public string? Subject { get; set; }
            public string? Result { get; set; }
        }
    }
}
