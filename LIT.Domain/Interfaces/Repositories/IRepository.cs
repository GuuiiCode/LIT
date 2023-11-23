using ControlExpenses.Domain.Entities;
using System.Linq.Expressions;

namespace LIT.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
        Task<bool> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
