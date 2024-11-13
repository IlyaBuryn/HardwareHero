namespace Aggregator.DTOs.Components
{
    public class ComponentMetricDto
    {
        public Guid Id { get; set; }
        public Guid? ComponentId { get; set; }
        public int ViewsCount { get; set; }
        public int ReviewCount { get; set; }
        public double Rating { get; set; }
        public decimal MinPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
