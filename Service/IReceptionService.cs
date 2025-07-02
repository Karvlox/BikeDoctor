namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IReceptionService : IGenericService<Reception, Guid>
{
    Task<IEnumerable<Reception>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}