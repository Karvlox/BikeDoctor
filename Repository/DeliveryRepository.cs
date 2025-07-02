namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class DeliveryRepository : GenericRepository<Delivery, Guid>, IDeliveryRepository
{
    public DeliveryRepository(BikeDoctorContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Delivery>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await _context.Deliveries
            .AsNoTracking()
            .Where(r => r.EmployeeCI == employeeCI)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}