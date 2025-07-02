namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class CostApprovalService : GenericService<CostApproval, Guid>, ICostApprovalService
{
    public CostApprovalService(ICostApprovalRepository repository) : base(repository)
    {
    }

    public async Task<IEnumerable<CostApproval>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await ((ICostApprovalRepository)_repository).GetAllByEmployeeCIAsync(employeeCI, pageNumber, pageSize);
    }

    public override async Task AddAsync(CostApproval costApproval)
    {
        ValidateCostApproval(costApproval);
        await base.AddAsync(costApproval);
    }

    public override async Task UpdateAsync(Guid id, CostApproval costApproval)
    {
        ValidateCostApproval(costApproval);
        await base.UpdateAsync(id, costApproval);
    }

    private void ValidateCostApproval(CostApproval costApproval)
    {
        if (costApproval.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(costApproval.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (costApproval.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }
}