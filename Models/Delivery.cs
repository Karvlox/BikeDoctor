namespace BikeDoctor.Models;

public class Delivery
{    
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set; }
    public bool SurveyCompleted { get; set; }

    public Client? Client { get; set; }
    public Motorcycle? Motorcycle { get; set; }
}