using Identity.Shared.Domain;

namespace Identity.Shared.Responses
{
    public class IdentityResponseRecords
    {
        public record FetchUserResponse(
            string UserId,
            string Email,
            string Username,
            IList<string>? Roles,
            DateTime? RegistrationDate,
            string? FirstName,
            string? FiLastName,
            string? Phone,
            Guid? RegionId,
            Guid? CurrencyId,
            string? ImageUrl,
            List<WishListComponent>? Favorites);
    }
}
