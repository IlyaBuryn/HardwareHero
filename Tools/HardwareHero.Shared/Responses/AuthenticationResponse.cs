using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Shared.Responses
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
    }
}
