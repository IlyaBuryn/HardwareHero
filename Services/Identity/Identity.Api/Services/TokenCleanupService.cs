using Identity.Api.Data;

namespace Identity.Api.Services
{
    public class TokenCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TokenCleanupService> _logger;

        public TokenCleanupService(
            ILogger<TokenCleanupService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanUpRevokedTokensAsync();

                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }

        private async Task CleanUpRevokedTokensAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _grantsDbContext = scope.ServiceProvider.GetRequiredService<GrantsDbContext>();

                var revokedTokens = await _grantsDbContext.RefreshTokens
                .Where(rt => rt.IsRevoked)
                .ToListAsync();

                _grantsDbContext.RefreshTokens.RemoveRange(revokedTokens);
                await _grantsDbContext.SaveChangesAsync();

                _logger.LogInformation($"Refresh tokens was deleted!");
            }
        }
    }
}
