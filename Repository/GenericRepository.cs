namespace BikeDoctor.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeDoctor.Data;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly BikeDoctorContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(BikeDoctorContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await _dbSet
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
