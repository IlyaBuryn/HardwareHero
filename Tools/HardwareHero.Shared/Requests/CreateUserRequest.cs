using HardwareHero.Shared.Models.Users;

namespace HardwareHero.Shared.Requests
{
    // TODO: ?
    public class CreateUserRequest
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
    }
}
