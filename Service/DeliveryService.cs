namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class DeliveryService : GenericService<Delivery, Guid>, IDeliveryService
{
    public DeliveryService(IDeliveryRepository repository) : base(repository)
    {
    }

    public override async Task AddAsync(Delivery delivery)
    {
        ValidateDelivery(delivery);
        await base.AddAsync(delivery);
    }

    public override async Task UpdateAsync(Guid id, Delivery delivery)
    {
        ValidateDelivery(delivery);
        if (id != delivery.Id)
            throw new ArgumentException("El ID no coincide.");
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
}