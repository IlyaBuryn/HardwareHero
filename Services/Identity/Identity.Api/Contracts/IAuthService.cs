using Identity.Shared.Domain;
using Identity.Shared.Requests;
using Identity.Shared.Responses;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Api.Contracts
{
    public interface IAuthService
    {
        Task<ApplicationUser> SignInAsync(SignInRequest model);
        Task<ApplicationUser> SignUpAsync(SignUpRequest model);

        Task<AuthenticationResponse> UpdatePasswordAsync(UserPasswordChangeRequest model);
        Task<AuthenticationResponse> RefreshTokenAsync(TokenRequest tokenRequest);
        Task<AuthenticationResponse?> VerifyTokenAsync(TokenRequest tokenRequest);
        Task<AuthenticationResponse> GenerateJwtTokenAsync(ApplicationUser user);
        Task<List<Claim>> GetAllValidClaimsAsync(ApplicationUser user);
    }
}
