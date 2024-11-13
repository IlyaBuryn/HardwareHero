using HardwareHero.Shared.Models;

namespace Identity.Shared.Domain
{
    public class WishListComponent : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
