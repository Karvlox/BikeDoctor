namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IDiagnosisRepository : IGenericRepository<Diagnosis, Guid>
{
    Task<IEnumerable<Diagnosis>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}