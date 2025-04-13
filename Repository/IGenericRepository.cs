namespace BikeDoctor.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<TEntity> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);
}