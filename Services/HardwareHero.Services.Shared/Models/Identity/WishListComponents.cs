using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareHero.Services.Shared.Models.Identity
{
    [Table("WishListComponents")]
    public class WishListComponents : BaseEntity
    { 
        public Guid ComponentId { get; set; }
    }
}
