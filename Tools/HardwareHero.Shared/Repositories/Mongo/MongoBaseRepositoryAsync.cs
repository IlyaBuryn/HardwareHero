using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;
using MongoDB.Driver;

namespace HardwareHero.Shared.Repositories.Mongo
{
    public class MongoBaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _collection;

        public MongoBaseRepositoryAsync(MongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<T>();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? (int)await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty)
                : (int)await _collection.CountDocumentsAsync(predicate);
        }

        public async Task<DataAnswer<T>> CreateEntityAsync(T entityToCreate)
        {
            if (entityToCreate == null)
            {
                return new(new ArgumentNullException(nameof(entityToCreate)));
            }

            await _collection.InsertOneAsync(entityToCreate);
            
            return new(entityToCreate);
        }

        public async Task<DataAnswer<T>> UpdateEntityAsync(T entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                return new(new ArgumentNullException(nameof(entityToUpdate)));
            }

            var filter = Builders<T>.Filter.Eq(e => e.Id, entityToUpdate.Id);
            var result = await _collection.ReplaceOneAsync(filter, entityToUpdate);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return new(entityToUpdate);
            }

            return new(new NotFoundException(nameof(entityToUpdate)));
        }

        public async Task<DataAnswer<T>> RemoveEntityAsync(Guid entityId)
        {
            if (entityId == Guid.Empty)
            {
                return new(new ArgumentNullException(nameof(entityId)));
            }

            var filter = Builders<T>.Filter.Eq(e => e.Id, entityId);
            var item = await _collection.FindAsync(filter);
            var result = await _collection.DeleteOneAsync(filter);

            if (result.IsAcknowledged && result.DeletedCount > 0)
            {
                return new(item.FirstOrDefault());
            }

            return new (new NotFoundException(nameof(entityId)));
        }

        public async Task<DataAnswer<IEnumerable<T>>> FindAllEntitiesAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            var entities = await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();

            return new DataAnswer<IEnumerable<T>>(entities);
        }

        public async Task<DataAnswer<T>> FindEntityAsync(Guid entityId, params Expression<Func<T, object>>[] includeProperties)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entityId);
            var entity = await _collection.Find(filter).FirstOrDefaultAsync();

            return entity != null
                ? new(entity)
                : new(new NotFoundException(nameof(entity)));
        }

        public async Task<DataAnswer<T>> FindEntityAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var entity = await _collection.Find(predicate).FirstOrDefaultAsync();

            return entity != null
                ? new(entity)
                : new(new NotFoundException(nameof(entity)));
        }       
    }
}
