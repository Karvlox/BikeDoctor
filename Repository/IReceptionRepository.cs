namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IReceptionRepository : IGenericRepository<Reception, Guid>
{
    Task<IEnumerable<Reception>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}
