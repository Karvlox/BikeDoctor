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
}