namespace HardwareHero.Shared.Requests
{
    public class AddRemoveRolesRequest
    {
        public string Username { get; set; }
        public string[] Roles { get; set; }
    }
}
