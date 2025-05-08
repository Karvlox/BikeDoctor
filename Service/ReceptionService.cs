namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class ReceptionService : GenericService<Reception, Guid>, IReceptionService
{
    public ReceptionService(IReceptionRepository repository) : base(repository)
    {
    }

    public override async Task AddAsync(Reception reception)
    {
        ValidateReception(reception);
        await base.AddAsync(reception);
    }

    public override async Task UpdateAsync(Guid id, Reception reception)
    {
        ValidateReception(reception);
        if (id != reception.Id)
            throw new ArgumentException("El ID no coincide.");
        await base.UpdateAsync(id, reception);
    }

    private void ValidateReception(Reception reception)
    {
        if (reception.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(reception.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (reception.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }
}