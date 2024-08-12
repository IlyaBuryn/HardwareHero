namespace Identity.Api.Records
{
    public class RequestModels
    {
        public record SignUpRequestModel(
            string Username,
            string Email,
            string Password,
            string? CallbackUrl,
            bool StayIn = false);

        public record SignInRequestModel(
            string UsernameOrEmail,
            string Password,
            string? CallbackUrl,
            bool StayIn = false);

        public class TokenRequest 
        {
            public string AccessToken {  get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
