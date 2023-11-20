using ControlExpenses.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;
using MongoDB.Driver;

namespace LIT.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public Repository(MongoDBContext context)
        {
            _collection = context.GetCollection<TEntity>();
        }

        public async Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _collection.Find(x => true).ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(entity, default, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        }
    }
}
