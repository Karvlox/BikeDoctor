namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class CostApprovalService : GenericService<CostApproval, Guid>, ICostApprovalService
{
    public CostApprovalService(ICostApprovalRepository repository) : base(repository)
    {
    }

    public override async Task AddAsync(CostApproval costApproval)
    {
        ValidateCostApproval(costApproval);
        await base.AddAsync(costApproval);
    }

    public override async Task UpdateAsync(Guid id, CostApproval costApproval)
    {
        ValidateCostApproval(costApproval);
        if (id != costApproval.Id)
            throw new ArgumentException("El ID no coincide.");
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
        if (costApproval.ListLaborCosts == null || !costApproval.ListLaborCosts.Any())
            throw new ArgumentException("La lista de costos de mano de obra no puede estar vacía.");
    }
}