using System.Linq.Expressions;
using RSS.Domain.Common;

namespace RSS.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity: BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);

    Task<TEntity?> GetByIdAsync(int id);

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    Task DeleteByIdAsync(int id);
}