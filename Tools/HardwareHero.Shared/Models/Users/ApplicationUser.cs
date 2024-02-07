using HardwareHero.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace HardwareHero.Shared.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public WishList? WishList { get; set; }

    }
}
