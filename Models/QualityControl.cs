namespace BikeDoctor.Models;

public class QualityControl
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public ICollection<Control>? ListControls { get; set; }

    public Client? Client { get; set; }
    public Motorcycle? Motorcycle { get; set; }
}