using Microsoft.AspNetCore.Identity;

namespace Identity.Shared.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageName { get; set; }
        public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow;
        public Guid? RegionId { get; set; }
        public Guid? CurrencyId { get; set; }

        public virtual ICollection<WishListComponent>? Components { get; set; }
    }
}
