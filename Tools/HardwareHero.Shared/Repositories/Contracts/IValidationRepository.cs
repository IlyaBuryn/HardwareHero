using HardwareHero.Filter.Operations;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface IValidationRepository<T> : ICrudRepositoryAsync<T> 
        where T : BaseEntity
    {
        void CheckIsAlreadyExist(Expression<Func<T, bool>> expression, Exception alreadyExistException);
        bool IsAlreadyExist(Expression<Func<T, bool>> expression);

        void CheckIsNotFound(Expression<Func<T, bool>> expression, Exception notFoundException);
        bool IsNotFound(Expression<Func<T, bool>> expression);

        void CheckPaginationOptions(IPaginable filter, Exception pageException);
    }
}
