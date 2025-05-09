namespace BikeDoctor.Models;

public class SpareParts
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; } 
    public int EmployeeCI { get; set; }
    public ICollection<SparePart>? ListSpareParts { get; set; }
    public bool Reviewed { get; set; } = false;
}