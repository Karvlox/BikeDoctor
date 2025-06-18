namespace BikeDoctor.DTOs;

using BikeDoctor.Models;

public class UpdateRepairDto
{
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public ICollection<Reparation>? ListReparations { get; set; }
    public bool Reviewed { get; set; } = false;
}