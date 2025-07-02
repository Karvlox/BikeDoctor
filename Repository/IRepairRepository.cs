namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IRepairRepository : IGenericRepository<Repair, Guid>
{
    Task<IEnumerable<Repair>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}