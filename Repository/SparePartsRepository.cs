namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class SparePartsRepository : GenericRepository<SpareParts, Guid>, ISparePartsRepository
{
    public SparePartsRepository(BikeDoctorContext context) : base(context)
    {
    }

    public override async Task<SpareParts> GetByIdAsync(Guid id)
    {
        return await _context.Set<SpareParts>()
            .Include(s => s.ListSpareParts)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}