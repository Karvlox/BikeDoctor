namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class RepairService : GenericService<Repair, Guid>, IRepairService
{
    public RepairService(IRepairRepository repository) : base(repository)
    {
    }

    public async Task<IEnumerable<Repair>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await ((IRepairRepository)_repository).GetAllByEmployeeCIAsync(employeeCI, pageNumber, pageSize);
    }

    public override async Task AddAsync(Repair repair)
    {
        ValidateRepair(repair);
        await base.AddAsync(repair);
    }

    public override async Task UpdateAsync(Guid id, Repair repair)
    {
        ValidateRepair(repair);
        await base.UpdateAsync(id, repair);
    }

    private void ValidateRepair(Repair repair)
    {
        if (repair.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(repair.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (repair.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }
}