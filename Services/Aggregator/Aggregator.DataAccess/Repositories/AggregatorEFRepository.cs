using Aggregator.DataAccess.Data;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories
{
    internal class AggregatorEFRepository<T> : EFCrudRepositoryAsync<T>, IPageRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly AggregatorDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public AggregatorEFRepository(AggregatorDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?> set, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var result = set.Skip(skip).Take(pageSize);

            return await result.ToListAsync();
        }
    }
}
