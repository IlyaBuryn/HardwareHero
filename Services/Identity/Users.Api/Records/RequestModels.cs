namespace Users.Api.Records
{
    public class RequestModels
    {
        public record CreateUserRequestModel(
            string UserName,
            string Email,
            string Password,
            string? FullName,
            Guid? RegionId,
            Guid? CurrencyId,
            string? Image);

        public record UpdateUserRequestModel(
            string? UserName,
            string? FullName,
            string? Image,
            Guid? RegionId,
            Guid? CurrencyId);
    }
}
