namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class QualityControlRepository : GenericRepository<QualityControl, Guid>, IQualityControlRepository
{
    public QualityControlRepository(BikeDoctorContext context) : base(context)
    {
    }

    public override async Task<QualityControl> GetByIdAsync(Guid id)
    {
        return await _context.Set<QualityControl>()
            .Include(q => q.ListControls)
            .Include(q => q.Client)
            .Include(q => q.Motorcycle)
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
    }
}