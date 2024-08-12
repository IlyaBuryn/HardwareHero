using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Shared.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow;
        public Guid? RegionId { get; set; }
        public Guid? CurrencyId { get; set; }

        public virtual ICollection<WishListComponents>? Components { get; set; }
    }
}
