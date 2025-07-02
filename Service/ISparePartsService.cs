namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface ISparePartsService : IGenericService<SpareParts, Guid>
{
    Task<IEnumerable<SpareParts>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}