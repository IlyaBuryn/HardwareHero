using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class ComputerServiceReviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ComputerServiceId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Recommended { get; set; }
        public Guid ContributorId { get; set; }
        public ComputerServiceDto ComputerService { get; set; }
        public ContributorDto Contributor { get; set; }
    }
}
