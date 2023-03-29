using System.ComponentModel;

namespace HardwareHero.Services.Shared.DTOs
{
    public class ComponentReviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ComponentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Recommended { get; set; }
        public string? ContributorName { get; set; }
        public string? ContributorLogo { get; set; }
        public Component? Component { get; set; }
    }
}
