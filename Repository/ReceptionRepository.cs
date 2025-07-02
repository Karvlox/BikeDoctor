namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;


public class ReceptionRepository : GenericRepository<Reception, Guid>, IReceptionRepository
{
    public ReceptionRepository(BikeDoctorContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Reception>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await _context.Receptions
            .AsNoTracking()
            .Where(r => r.EmployeeCI == employeeCI)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}