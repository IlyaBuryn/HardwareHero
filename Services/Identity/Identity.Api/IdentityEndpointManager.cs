using Identity.Api.Contracts;
using KafkaEventStream.Contracts;
using System.Text.Json;
using static Identity.Api.Records.RequestModels;

namespace Identity.Api
{
    public class IdentityEndpointManager : EventEndpointManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;

        public IdentityEndpointManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _authService = _serviceProvider.GetService<IAuthService>();
        }

        public override async Task<string> InvokeByEndpoint(string endpoint)
        {
            if (IsMatchEndpointsAndPopulate(endpoint, "refresh-token/{AccessToken}/{RefreshToken}",
                out TokenRequest tokens))
            {
                AuthenticationResponse result;
                try
                {
                    result = await _authService.RefreshTokenAsync(tokens);
                }
                catch (Exception ex)
                {
                    result = new AuthenticationResponse
                    {
                        IsSuccessful = false,
                        Errors = new List<string>() { ex.Message },
                    };
                }

                return JsonSerializer.Serialize(result);
            }

            return string.Empty;
        }
    }
}
