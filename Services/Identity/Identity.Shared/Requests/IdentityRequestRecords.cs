using Microsoft.AspNetCore.Http;

namespace Identity.Shared.Requests
{
    public class IdentityRequestRecords
    {
        public record CreateUserRequest(
            string UserName,
            string Email,
            string Password,
            string? FirstName,
            string? LastName,
            Guid? RegionId,
            Guid? CurrencyId,
            IFormFile? ImageData);

        public record UpdateUserRequest(
            string UserId,
            string? UserName,
            string? FirstName,
            string? LastName,
            string? Phone,
            Guid? RegionId,
            Guid? CurrencyId,
            IFormFile? ImageData);

        public record UserPasswordChangeRequest(
            string Email,
            string OldPassword,
            string NewPassword);

        public record AddRemoveRolesRequest(
            string Username,
            string[] Roles);

        public record AddRemoveRoleRequest(
            string Username,
            string Role);

        public record SignUpRequest(
            string Username,
            string Email,
            string Password,
            string? CallbackUrl,
            bool StayIn = false);

        public record SignInRequest(
            string UsernameOrEmail,
            string Password,
            string? CallbackUrl,
            bool StayIn = false);
    }
}
