namespace BikeDoctor.Service;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGenericService<TEntity, TKey> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<TEntity> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TKey id, TEntity entity);
    Task DeleteAsync(TKey id);
}