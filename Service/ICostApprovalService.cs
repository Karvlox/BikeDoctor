namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface ICostApprovalService : IGenericService<CostApproval, Guid>
{
    Task<IEnumerable<CostApproval>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}