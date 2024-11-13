using HardwareHero.Shared.Models;
using Prices.DTOs.Prices;

namespace Prices.BusinessLogic.Models
{
    public class ContributorComponentPrices : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public Guid ContributorId { get; set; }
        public List<PriceStamp> Prices { get; set; } = new();
        public bool IsUnsupported { get; set; } = false;
    }
}
