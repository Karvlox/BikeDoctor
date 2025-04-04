namespace BikeDoctor.Models;

public class Motorcycle
{
    public Guid Id { get; set;}
    public int ClientCI { get; set;}
    public string Brand { get; set;}
    public string Model { get; set;}
    public string LicensePlateNumber { get; set;}
    public string Color { get; set;}

    public Client? Client { get; set; }
}