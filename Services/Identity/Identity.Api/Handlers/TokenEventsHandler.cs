using EventDriven.Kafka.Services;
using EventDriven.Shared.Services;
using Identity.Api.Contracts;
using Identity.Shared.Events;

namespace Identity.Api.Handlers
{
    public class TokenEventsHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReplyService<TokenRequestEvent, AuthResultEvent> _tokenService;

        public TokenEventsHandler(
            IServiceProvider serviceProvider, 
            IReplyService<TokenRequestEvent, AuthResultEvent> tokenService)
        {
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _tokenService.HandleRequestAsync(async tokensEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
                    var result = await authService.RefreshTokenAsync(tokensEvent.Tokens);

                    return new AuthResultEvent()
                    {
                        AuthenticationResponse = result,
                    };
                }
            }, stoppingToken);
        }
    }
}
