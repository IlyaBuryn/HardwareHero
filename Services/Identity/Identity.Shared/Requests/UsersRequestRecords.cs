namespace Identity.Shared.Requests
{
    public class UsersRequestRecords
    {
        public record RolesRequest(
            List<string> Roles);

        public record UserRolesRequest(
            List<string> Roles, string UserId);
    }
}
