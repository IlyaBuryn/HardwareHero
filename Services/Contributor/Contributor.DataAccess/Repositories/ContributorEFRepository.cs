using Contributor.DataAccess.Data;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace Contributor.DataAccess.Repositories
{
    public class ContributorEFRepository<T> : EFCrudRepositoryAsync<T>, ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly ContributorDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public ContributorEFRepository(ContributorDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
    }
}
