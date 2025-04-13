namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class DiagnosisRepository : GenericRepository<Diagnosis, Guid>, IDiagnosisRepository
{
    public DiagnosisRepository(BikeDoctorContext context) : base(context)
    {
    }

    public override async Task<Diagnosis> GetByIdAsync(Guid id)
    {
        return await _context.Set<Diagnosis>()
            .Include(d => d.ListDiagnostics)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }
}