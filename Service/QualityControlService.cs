namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class QualityControlService : GenericService<QualityControl, Guid>, IQualityControlService
{
    public QualityControlService(IQualityControlRepository repository) : base(repository)
    {
    }

    public async Task<IEnumerable<QualityControl>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await ((IQualityControlRepository)_repository).GetAllByEmployeeCIAsync(employeeCI, pageNumber, pageSize);
    }

    public override async Task AddAsync(QualityControl qualityControl)
    {
        ValidateQualityControl(qualityControl);
        await base.AddAsync(qualityControl);
    }

    public override async Task UpdateAsync(Guid id, QualityControl qualityControl)
    {
        ValidateQualityControl(qualityControl);
        await base.UpdateAsync(id, qualityControl);
    }

    private void ValidateQualityControl(QualityControl qualityControl)
    {
        if (qualityControl.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(qualityControl.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (qualityControl.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }
}