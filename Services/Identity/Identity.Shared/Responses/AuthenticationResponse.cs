namespace Identity.Shared.Responses
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
    }
}
