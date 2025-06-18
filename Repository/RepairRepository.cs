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
}