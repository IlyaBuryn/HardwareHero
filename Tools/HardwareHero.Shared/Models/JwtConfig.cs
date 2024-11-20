namespace HardwareHero.Shared.Models
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string JwtCookieKey { get; set; }
        public string RefreshCookieKey { get; set; }
    }
}
