using HardwareHero.Services.Shared.Models.UserManagementService;

namespace HardwareHero.Services.Shared.Requests
{
    // TODO: ?
    public class CreateUserRequest
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
    }
}
