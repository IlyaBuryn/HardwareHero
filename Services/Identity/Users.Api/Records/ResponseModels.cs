namespace Users.Api.Records
{
    public class ResponseModels
    {
        public record GetUserResponseModel(
            string UserId,
            string Email,
            IList<string>? Roles,
            string Username,
            string? FullName,
            string? Phone,
            Guid? RegionId,
            Guid? CurrencyId,
            string? Image,
            List<WishListComponents>? Favorites);
    }
}
