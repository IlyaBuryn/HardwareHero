using static Identity.Api.Records.RequestModels;

namespace Identity.Api.Contracts
{
    public interface IAuthService
    {
        Task<ApplicationUser> SignInAsync(SignInRequestModel model);
        Task<ApplicationUser> SignUpAsync(SignUpRequestModel model);
        Task<AuthenticationResponse> RefreshTokenAsync(TokenRequest tokenRequest);
        Task<AuthenticationResponse?> VerifyTokenAsync(TokenRequest tokenRequest);
        Task<AuthenticationResponse> GenerateJwtTokenAsync(ApplicationUser user);
        Task<List<Claim>> GetAllValidClaimsAsync(ApplicationUser user);
    }
}
