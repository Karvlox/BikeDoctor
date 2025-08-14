namespace BikeDoctor.Service;

using System.Threading.Tasks;
using BikeDoctor.Models;
using BikeDoctor.Repository;

public class DeliveryService : GenericService<Delivery, Guid>, IDeliveryService
{
    public DeliveryService(IDeliveryRepository repository)
        : base(repository) { }

    public async Task<IEnumerable<Delivery>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await ((IDeliveryRepository)_repository).GetAllByEmployeeCIAsync(
            employeeCI,
            pageNumber,
            pageSize
        );
    }

    public override async Task AddAsync(Delivery delivery)
    {
        ValidateDelivery(delivery);
        await base.AddAsync(delivery);
    }

    public override async Task UpdateAsync(Guid id, Delivery delivery)
    {
        ValidateDelivery(delivery);
        await base.UpdateAsync(id, delivery);
    }

    private void ValidateDelivery(Delivery delivery)
    {
        if (delivery.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(delivery.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (delivery.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }

    public async Task<object> GetDeliveryMetricsAsync()
    {
        var deliveries = await _repository.GetAllAsync();

        int total = deliveries.Count();
        int terminados = deliveries.Count(d => d.Reviewed);
        int noTerminados = total - terminados;
        int conFormulario = deliveries.Count(d => d.SurveyCompleted);

        double porcentajeTerminados = total > 0 ? (terminados * 100.0 / total) : 0;
        double porcentajeFormulario = total > 0 ? (conFormulario * 100.0 / total) : 0;

        return new
        {
            total,
            terminados,
            noTerminados,
            porcentajeTerminados = Math.Round(porcentajeTerminados, 2),
            conFormulario,
            porcentajeFormulario = Math.Round(porcentajeFormulario, 2),
        };
    }
}
