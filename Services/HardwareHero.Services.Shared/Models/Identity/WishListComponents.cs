using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareHero.Services.Shared.Models.Identity
{
    public class WishListComponents : BaseEntity
    { 
        public Guid ComponentId { get; set; }
    }
}
