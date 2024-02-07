namespace HardwareHero.Shared.Responses
{
    public class AvgReviewsMarksResponse
    {
        public decimal AvgLocalReviewMark { get; set; }
        public int TotalLocalReviewCount { get; set; }
        public Dictionary<bool, int>? LocalReviewRecommendations { get; set; }
        public Dictionary<int, int>? LocalReviewRatings { get; set; }

        public decimal AvgGlobalReviewMark { get; set; }
        public int TotalGlobalReviewCount { get; set; }

        public decimal AvgReviewMark { get; set; }
        public int TotalReviewCount { get; set; }

    }
}
