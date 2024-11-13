using HardwareHero.Shared.Repositories.Contracts;

namespace HardwareHero.Shared.Extensions.Repository
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// This extension already using DataAnswerCheck()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <param name="predicate"></param>
        /// <param name="includeProps"></param>
        /// <returns></returns>
        public async static Task<T> NotFoundCheckAsync<T>(
            this IBaseRepositoryAsync<T> repo,
            Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includeProps) where T : BaseEntity
        {
            var entity = await repo.FindEntityAsync(predicate, includeProps);
            entity.DataAnswerCheck();

            var value = entity.Value;
            ThrowNotFound(value);

            return value!;
        }

        /// <summary>
        /// This extension already using DataAnswerCheck()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <param name="predicate"></param>
        /// <param name="includeProps"></param>
        /// <returns></returns>
        public async static Task<T> AlreadyExistCheckAsync<T>(
            this IBaseRepositoryAsync<T> repo,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProps) where T : BaseEntity
        {
            var entity = await repo.FindEntityAsync(predicate, includeProps);
            entity.DataAnswerCheck();

            var value = entity.Value;
            ThrowAlreadyExist(value);

            return value!;
        }

        private static void ThrowNotFound(BaseEntity? entity)
        {
            if (entity == null)
            {
                throw new NotFoundException(nameof(entity));
            }
        }

        private static void ThrowAlreadyExist(BaseEntity? entity)
        {
            if (entity != null)
            {
                throw new AlreadyExistException(entity);
            }
        }
    }
}
