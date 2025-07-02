namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IQualityControlRepository : IGenericRepository<QualityControl, Guid>
{
    Task<IEnumerable<QualityControl>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}