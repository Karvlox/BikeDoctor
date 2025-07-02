namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IDeliveryRepository : IGenericRepository<Delivery, Guid>
{
    Task<IEnumerable<Delivery>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}