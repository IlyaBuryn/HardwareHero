using Identity.Api.Contracts;
using Identity.Shared.Requests;
using Identity.Shared.Responses;
using System.Text.Json;

namespace Identity.Api
{
    public class IdentityEndpointManager
    {
        // TODO: change to handler
        //private readonly IServiceProvider _serviceProvider;
        //private readonly IAuthService _authService;

        //public IdentityEndpointManager(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //    _authService = _serviceProvider.GetService<IAuthService>();
        //}

        //public override async Task<string> InvokeByEndpoint(string endpoint)
        //{
        //    if (IsMatchEndpointsAndPopulate(endpoint, "refresh-token/{AccessToken}/{RefreshToken}",
        //        out TokenRequest tokens))
        //    {
        //        AuthenticationResponse result;
        //        try
        //        {
        //            result = await _authService.RefreshTokenAsync(tokens);
        //        }
        //        catch (Exception ex)
        //        {
        //            result = new AuthenticationResponse
        //            {
        //                IsSuccessful = false,
        //                Errors = new List<string>() { ex.Message },
        //            };
        //        }

        //        return JsonSerializer.Serialize(result);
        //    }

        //    return string.Empty;
        //}
    }
}
