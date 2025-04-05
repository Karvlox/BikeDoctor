namespace BikeDoctor.Models;

public class Reception
{
    public Guid Id { get; set;}
    public DateTime Date { get; set;}
    public int CliendCI { get; set;}
    public string MotorcycleLicensePlate { get; set;}
    public int EmployeeCI { get; set;}
    public List<string> Reasons { get; set;}
    public List<string> Images { get; set;}
    
}