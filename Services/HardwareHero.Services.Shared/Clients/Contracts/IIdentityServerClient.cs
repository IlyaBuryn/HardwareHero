using HardwareHero.Services.Shared.IdentityServer;
using HardwareHero.Services.Shared.Options;

namespace HardwareHero.Services.Shared.Clients.Contracts
{
    public interface IIdentityServerClient
    {
        Task<Token> GetApiTokenAsync(IdentityServerApiOptions options);
    }
}
