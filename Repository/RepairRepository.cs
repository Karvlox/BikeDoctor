namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class RepairRepository : GenericRepository<Repair, Guid>, IRepairRepository
{
    public RepairRepository(BikeDoctorContext context) : base(context)
    {
    }

    public override async Task<Repair> GetByIdAsync(Guid id)
    {
        return await _context.Set<Repair>()
            .Include(r => r.ListReparations)
            .Include(r => r.Client)
            .Include(r => r.Motorcycle)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}