using Microsoft.AspNetCore.Identity;

namespace Identity.Shared.Responses
{
    public class UsersResponseRecords
    {
        public record RolesResponse(
            Dictionary<string, string?> Result);
    }
}
