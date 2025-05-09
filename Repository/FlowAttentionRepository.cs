namespace BikeDoctor.Repositories
{
    using BikeDoctor.Data;
    using BikeDoctor.Models;
    using BikeDoctor.Repository;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FlowAttentionRepository : IFlowAttentionRepository
    {
        private readonly BikeDoctorContext _context;
        private readonly DbSet<FlowAttention> _dbSet;

        public FlowAttentionRepository(BikeDoctorContext context)
        {
            _context = context;
            _dbSet = _context.Set<FlowAttention>();
        }

        public async Task<IEnumerable<FlowAttention>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _dbSet
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<FlowAttention?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<FlowAttention?> GetByClientAndLicenseAsync(int clientCI, string licensePlate)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(fa => fa.ClientCI == clientCI && fa.MotorcycleLicensePlate == licensePlate)
                .FirstOrDefaultAsync();
        }

        public async Task<FlowAttention?> GetByClientCIAsync(int clientCI)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(fa => fa.ClientCI == clientCI)
                .FirstOrDefaultAsync();
        }

        public async Task<FlowAttention?> GetByEmployeeCIAsync(int employeeCI)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(fa => fa.EmployeeCI == employeeCI)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(FlowAttention flowAttention)
        {
            await _dbSet.AddAsync(flowAttention);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FlowAttention flowAttention)
        {
            _dbSet.Update(flowAttention);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var flowAttention = await GetByIdAsync(id);
            if (flowAttention != null)
            {
                _dbSet.Remove(flowAttention);
                await _context.SaveChangesAsync();
            }
        }
    }
}