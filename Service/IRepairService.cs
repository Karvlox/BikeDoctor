namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IRepairService : IGenericService<Repair, Guid>
{
    Task<IEnumerable<Repair>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}