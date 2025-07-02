namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IQualityControlService : IGenericService<QualityControl, Guid>
{
    Task<IEnumerable<QualityControl>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}