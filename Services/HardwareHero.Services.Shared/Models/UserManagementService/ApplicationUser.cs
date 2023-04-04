using HardwareHero.Services.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HardwareHero.Services.Shared.Models.UserManagementService
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
        public WishList? WishList { get; set; }

    }
}
