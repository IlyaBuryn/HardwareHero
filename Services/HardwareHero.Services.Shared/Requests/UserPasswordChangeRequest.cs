namespace HardwareHero.Services.Shared.Requests
{
    // TODO: ?
    public class UserPasswordChangeRequest
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
