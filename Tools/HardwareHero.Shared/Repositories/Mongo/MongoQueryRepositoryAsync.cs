using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;
using MongoDB.Driver;

namespace HardwareHero.Shared.Repositories.Mongo
{
    public class MongoQueryRepositoryAsync<T> : MongoBaseRepositoryAsync<T>, IQueryRepositoryAsync<T>
        where T : BaseEntity
    {
        public MongoQueryRepositoryAsync(IMongoDatabase database, string collectionName)
            : base(database, collectionName) { }

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
