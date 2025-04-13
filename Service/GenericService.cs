namespace BikeDoctor.Service;

using BikeDoctor.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenericService<TEntity, TKey> : IGenericService<TEntity, TKey> where TEntity : class
{
    protected readonly IGenericRepository<TEntity, TKey> _repository;

    public GenericService(IGenericRepository<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await _repository.GetAllAsync(pageNumber, pageSize);
    }
    public virtual async Task<TEntity> GetByIdAsync(TKey id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"No se encontró la entidad con ID {id}.");
        }
        return entity;
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        await _repository.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TKey id, TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        await _repository.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"No se encontró la entidad con ID {id}.");
        }
        await _repository.DeleteAsync(id);
    }
}