using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using BikeDoctor.Data;
using BikeDoctor.Models;
namespace BikeDoctor.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly BikeDoctorContext _context;

    public MotorcycleRepository(BikeDoctorContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
    {
        return await _context.Motorcycles.ToListAsync();
    }

    public async Task<Motorcycle> GetMotorcyclesByIdAsync(Guid id)
    {
        return await _context.Motorcycles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Motorcycle> GetMotorcycleByLicensePlateNumberAsync(string licensePlateNumber)
    {
        return await _context.Motorcycles.FirstOrDefaultAsync(c => c.LicensePlateNumber == licensePlateNumber);
    }

    public async Task AddMotorcycleAsync(Motorcycle motorcycle)
    {
        await _context.Motorcycles.AddAsync(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMotorcycleAsync(Motorcycle motorcycle)
    {
        _context.Motorcycles.Update(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMotorcycleAsync(Guid id)
    {
        var motorcycle = await _context.Motorcycles.FindAsync(id);
        if (motorcycle != null)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteMotorcycleByLicensePlateNumberAsync(string licensePlateNumber)
    {
        var motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(c => c.LicensePlateNumber == licensePlateNumber);
        if (motorcycle != null)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }
    }
}