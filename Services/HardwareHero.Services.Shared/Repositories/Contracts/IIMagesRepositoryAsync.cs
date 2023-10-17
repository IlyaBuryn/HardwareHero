using HardwareHero.Services.Shared.Models;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface IIMagesRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<string> SaveImageAsync(T entity, byte[] imageData, Expression<Func<T, string>>[] fileNameParts);
        Task<string> DeleteImageAsync(T entity, Expression<Func<T, string>>[] fileNameParts);
    }
}
