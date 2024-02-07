using HardwareHero.Shared.Repositories;
using HardwareHero.Shared.Repositories.Contracts;

namespace HardwareHero.Shared.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<T> GetOneWithNotFoundCheck<T>
            (this ICrudRepositoryAsync<T> repo,
            Expression<Func<T, bool>> expression,
            bool isAllInclude = true) where T : BaseEntity
        {
            var includesFilter = new IncludeProperties<T>(isAllInclude);
            var result = (await repo.GetOneEntityAsync(expression, includesFilter))
                .CheckIfObjectNotFound();

            return result!;
        }

        public static async Task<IQueryable<T?>> GetManyWithDefaultOrEmptyCheckAsync<T>
            (this ICrudRepositoryAsync<T> repo,
            Expression<Func<T, bool>> expression,
            bool isAllInclude = true) where T : BaseEntity
        {
            var includesFilter = new IncludeProperties<T>(isAllInclude);
            var result = (await repo.GetManyEntitiesAsync(expression, includesFilter))
                .CheckIfObjectNotFound();

            return result!;
        }


        public static void CheckIfObjectAlreadyExist<T>
            (this IValidationRepository<T> repo, Expression<Func<T, bool>> expression, string? entity = null) where T : BaseEntity
        {
            repo.CheckIsAlreadyExist(expression, entity == null ? new AlreadyExistException<T>() :
                new AlreadyExistException<T>(entity));
        }

        public static void CheckIfObjectNotFound<T>
            (this IValidationRepository<T> repo, Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            repo.CheckIsNotFound(expression,
                new NotFoundException(nameof(T)));
        }

        public static void CheckPaginationOptions<T>
            (this IValidationRepository<T> repo, PaginationInfo paginationInfo) where T : BaseEntity
        {
            repo.CheckPaginationOptions(paginationInfo,
                new PageOptionsValidationException());
        }


        public static T? CheckIfObjectAlreadyExist<T>(this T? obj)
        {
            if (obj != null)
            {
                throw new AlreadyExistException<T>();
            }

            return obj;
        }

        public static T CheckIfObjectNotFound<T>(this T? obj)
        {
            if (obj == null)
            {
                throw new NotFoundException(nameof(obj));
            }

            return obj;
        }
    }
}
