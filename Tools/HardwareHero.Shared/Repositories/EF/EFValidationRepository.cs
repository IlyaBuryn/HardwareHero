using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Shared.Repositories.EF
{
    public class EFValidationRepository<T> : EFCrudRepositoryAsync<T>, IValidationRepository<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFValidationRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void CheckIsAlreadyExist(Expression<Func<T, bool>> expression, Exception alreadyExistException)
        {
            if (IsAlreadyExist(expression))
            {
                throw alreadyExistException;
            }
        }

        public void CheckIsNotFound(Expression<Func<T, bool>> expression, Exception notFoundException)
        {
            if (IsNotFound(expression))
            {
                throw notFoundException;
            }
        }

        public void CheckPaginationOptions(PaginationInfo paginationInfo, Exception pageException)
        {
            if (paginationInfo.PageNumber <= 0 || paginationInfo.PageSize <= 0)
            {
                throw pageException;
            }
        }

        public bool IsAlreadyExist(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet.Where(expression);

            return query.Count() > 0;
        }

        public bool IsNotFound(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet.Where(expression);

            return query.Count() == 0;
        }
    }
}
