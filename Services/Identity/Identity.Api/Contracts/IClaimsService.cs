namespace Identity.Api.Contracts
{
    public interface IClaimsService
    {
        Task<IList<Claim>> GetClaimsAsync(string userId);
        Task<bool> AddClaimsAsync(string userId, string claimName, string claimValue);
    }
}
