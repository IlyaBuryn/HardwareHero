using HardwareHero.Services.Shared.Infrastructure;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface IValidationRepository<T> : ICrudRepositoryAsync<T> 
        where T : BaseEntity
    {
        void CheckIsAlreadyExist(Expression<Func<T, bool>> expression, Exception alreadyExistException);
        bool IsAlreadyExist(Expression<Func<T, bool>> expression);

        void CheckIsNotFound(Expression<Func<T, bool>> expression, Exception notFoundException);
        bool IsNotFound(Expression<Func<T, bool>> expression);

        void CheckPaginationOptions(PaginationInfo paginationInfo, Exception pageException);
    }
}
