namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IDeliveryService : IGenericService<Delivery, Guid>
{
    Task<IEnumerable<Delivery>> GetAllByEmployeeCIAsync(int employeeCI, int pageNumber = 1, int pageSize = 10);
}