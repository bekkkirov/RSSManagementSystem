using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RSS.Application.Exceptions;
using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Common;

namespace RSS.Infrastructure.Persistence.DataAccess;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity: BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(RssContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter is null)
        {
            return await _dbSet.AsNoTracking()
                               .ToListAsync();
        }

        return await _dbSet.Where(filter)
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        if (entityToDelete is null)
        {
            throw new NotFoundException("Entity with specified id wasn't found.");
        }

        _dbSet.Remove(entityToDelete);
    }
}