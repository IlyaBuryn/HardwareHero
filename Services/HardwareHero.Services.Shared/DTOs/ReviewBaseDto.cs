namespace HardwareHero.Services.Shared.DTOs
{
    public class ReviewBaseDto
    {
        public DateTime Timestamp { get; set; }
        public int? Rating { get; set; }
        public bool? IsRecommended { get; set; }
        public string Text { get; set; }
    }
}
