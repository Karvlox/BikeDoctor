namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IDiagnosisService : IGenericService<Diagnosis, Guid>
{
    Task<IEnumerable<Diagnosis>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}