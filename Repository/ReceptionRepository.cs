namespace BikeDoctor.Repositories;

using BikeDoctor.Data;
using BikeDoctor.Models;
using BikeDoctor.Repository;

public class ReceptionRepository : GenericRepository<Reception, Guid>, IReceptionRepository
{
    public ReceptionRepository(BikeDoctorContext context) : base(context)
    {
    }
}