using HardwareHero.Services.Shared.Models.Contributor;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComputerServiceReview : BaseEntity
    {
        public string Name { get; set; }
        public Guid ComputerServiceId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Recommended { get; set; }
        public Guid ContributorId { get; set; }

        public virtual ComputerService ComputerService { get; set; }
        public virtual ContributorModel Contributor { get; set; }
    }
}
