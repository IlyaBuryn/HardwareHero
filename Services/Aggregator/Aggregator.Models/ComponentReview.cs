using HardwareHero.Services.Shared.Models;

namespace Aggregator.Models
{
    public class ComponentReview : BaseEntity 
    {
        public string Name { get; set; }
        public Guid ComponentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Recommended { get; set; }
        public string? ContributorName { get; set; }
        public string? ContributorLogo { get; set; }

        public virtual Component Component { get; set; }
    }
}
