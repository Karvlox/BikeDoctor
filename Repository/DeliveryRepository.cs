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

    public override async Task<Delivery> GetByIdAsync(Guid id)
    {
        return await _context.Set<Delivery>()
            .Include(d => d.Client)
            .Include(d => d.Motorcycle)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }
}