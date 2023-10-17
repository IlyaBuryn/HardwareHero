﻿namespace HardwareHero.Services.Shared.Models
{
    public class ReviewBase : BaseEntity
    {
        public DateTime Timestamp { get; set; }
        public int? Rating { get; set; }
        public bool? IsRecommended { get; set; }
        public string Text { get; set; }
    }
}
