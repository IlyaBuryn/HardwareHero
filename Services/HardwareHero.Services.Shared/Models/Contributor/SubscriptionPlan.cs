﻿using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class SubscriptionPlan : BaseEntity
    {
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int PriorityLevel { get; set; }
    }
}