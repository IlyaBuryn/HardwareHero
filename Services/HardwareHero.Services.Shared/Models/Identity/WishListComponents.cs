using System.ComponentModel.DataAnnotations.Schema;
using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Identity
{
    public class WishListComponents : BaseEntity
    { 
        public Guid ComponentId { get; set; }
    }
}
