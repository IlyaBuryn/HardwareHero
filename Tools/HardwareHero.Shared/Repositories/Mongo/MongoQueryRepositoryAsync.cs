using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;

namespace HardwareHero.Shared.Repositories.Mongo
{
    public class MongoQueryRepositoryAsync<T> : MongoBaseRepositoryAsync<T>, IQueryRepositoryAsync<T>
        where T : BaseEntity
    {
        public MongoQueryRepositoryAsync(MongoDbContext context)
            : base(context) { }

        public Task<DataAnswer<IQueryable<T>>> FindAsync(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            // TODO: --
            throw new NotImplementedException();
        }

        public Task<PagedDataAnswer<T>> FindPagedAsync(Func<T, bool>? predicate, IPaginable filter, params Expression<Func<T, object>>[] includeProperties)
        {
            // TODO: --
            throw new NotImplementedException();
        }
    }
}
