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
}