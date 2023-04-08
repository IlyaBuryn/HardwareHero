using HardwareHero.Services.Shared.Models;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface IPageRepositoryAsync<T> : ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<IQueryable<T?>> GetPageAsync(IQueryable<T?> set, int pageNumber, int pageSize);
    }
}
