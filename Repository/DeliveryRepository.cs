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
}