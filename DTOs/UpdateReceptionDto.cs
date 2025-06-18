namespace BikeDoctor.DTOs;

public class UpdateReceptionDto
{
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set; }
    public ICollection<string>? Reasons { get; set; }
    public ICollection<string>? Images { get; set; }
    public bool Reviewed { get; set; }
}