namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class CostApprovalRepository : GenericRepository<CostApproval, Guid>, ICostApprovalRepository
{
    public CostApprovalRepository(BikeDoctorContext context) : base(context)
    {        
    }
    
    public async Task<IEnumerable<CostApproval>> GetAllByEmployeeCIAsync(
    int employeeCI,
    int pageNumber = 1,
    int pageSize = 10
)
    {
        return await _context.CostApprovals
            .AsNoTracking()
            .Where(r => r.EmployeeCI == employeeCI)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}