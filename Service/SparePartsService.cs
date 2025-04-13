namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class SparePartsService : GenericService<SpareParts, Guid>, ISparePartsService
{
    public SparePartsService(ISparePartsRepository repository) : base(repository)
    {
    }

    public override async Task AddAsync(SpareParts spareParts)
    {
        ValidateSpareParts(spareParts);
        await base.AddAsync(spareParts);
    }

    public override async Task UpdateAsync(Guid id, SpareParts spareParts)
    {
        ValidateSpareParts(spareParts);
        if (id != spareParts.Id)
            throw new ArgumentException("El ID no coincide.");
        await base.UpdateAsync(id, spareParts);
    }

    private void ValidateSpareParts(SpareParts spareParts)
    {
        if (spareParts.CliendCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(spareParts.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (spareParts.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
        if (spareParts.ListSpareParts == null || !spareParts.ListSpareParts.Any())
            throw new ArgumentException("La lista de repuestos no puede estar vacía.");
    }
}