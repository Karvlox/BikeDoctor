namespace BikeDoctor.Models;

public class Repair
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public ICollection<Reparation>? ListReparations { get; set; }
    public bool Reviewed { get; set; } = false;

    public Client? Client { get; set; }
    public Motorcycle? Motorcycle { get; set; }
}