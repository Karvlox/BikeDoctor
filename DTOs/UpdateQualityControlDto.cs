namespace BikeDoctor.DTOs;

using BikeDoctor.Models;

public class UpdateQualityControlDto
{    
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public ICollection<Control>? ListControls { get; set; }
    public bool Reviewed { get; set; } = false;
}