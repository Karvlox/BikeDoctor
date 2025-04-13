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

    public override async Task<CostApproval> GetByIdAsync(Guid id)
    {
        return await _context.Set<CostApproval>()
            .Include(ca => ca.ListLaborCosts)
            .Include(ca => ca.Client)
            .Include(ca => ca.Motorcycle)
            .AsNoTracking()
            .FirstOrDefaultAsync(ca => ca.Id == id);
    }
}