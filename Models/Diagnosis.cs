namespace BikeDoctor.Models;

public class Diagnosis
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public List<Diagnostic> ListDiagnostics { get; set; }
    
}

public class Diagnostic
{
    public Guid Id { get; set;}
    public string Error { get; set; }
    public string DetailOfError { get; set; }
    public string ServiceType  { get; set; }
    public int TimeSpent { get; set; } // minutes
}