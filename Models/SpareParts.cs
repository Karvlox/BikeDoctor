namespace BikeDoctor.Models;

public class SpareParts
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int CliendCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set; }
    public List<SparePart> ListSpareParts { get; set; }
}

public class SparePart
{
    public Guid Id {get; set; }
    public string NameSparePart { get; set; }
    public string DetailSparePart { get; set; }
    public int Price { get; set; }
}