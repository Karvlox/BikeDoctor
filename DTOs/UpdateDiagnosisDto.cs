namespace BikeDoctor.DTOs;

using BikeDoctor.Models;

public class UpdateDiagnosisDto
{
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public ICollection<Diagnostic>? ListDiagnostics { get; set; } = new List<Diagnostic>();
    public bool Reviewed { get; set; } = false;
}