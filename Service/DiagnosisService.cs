namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Threading.Tasks;

public class DiagnosisService : GenericService<Diagnosis, Guid>, IDiagnosisService
{
    public DiagnosisService(IDiagnosisRepository repository) : base(repository)
    {
    }

    public override async Task AddAsync(Diagnosis diagnosis)
    {
        ValidateDiagnosis(diagnosis);
        await base.AddAsync(diagnosis);
    }

    public override async Task UpdateAsync(Guid id, Diagnosis diagnosis)
    {
        ValidateDiagnosis(diagnosis);
        await base.UpdateAsync(id, diagnosis);
    }

    private void ValidateDiagnosis(Diagnosis diagnosis)
    {
        if (diagnosis.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(diagnosis.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (diagnosis.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }
}