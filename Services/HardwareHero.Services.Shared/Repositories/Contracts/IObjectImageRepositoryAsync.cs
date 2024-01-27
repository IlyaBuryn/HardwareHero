using HardwareHero.Services.Shared.Infrastructure;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface IObjectImageRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<string> SaveImageAsync(T entity, byte[] imageData, string fileName);
        Task<string> DeleteImageAsync(T entity, string fileName);
    }
}
