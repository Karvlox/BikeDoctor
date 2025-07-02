namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface ICostApprovalRepository : IGenericRepository<CostApproval, Guid>
{
    Task<IEnumerable<CostApproval>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}