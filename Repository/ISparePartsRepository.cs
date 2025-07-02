namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface ISparePartsRepository : IGenericRepository<SpareParts, Guid>
{
    Task<IEnumerable<SpareParts>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}