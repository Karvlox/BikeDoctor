namespace BikeDoctor.Models;

public class Diagnosis
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public ICollection<Diagnostic>? ListDiagnostics { get; set; }
    public bool Reviewed { get; set; } = false;
}