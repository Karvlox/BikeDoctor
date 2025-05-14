namespace BikeDoctor.DTOs;

using BikeDoctor.Models;

public class UpdateCostApprovalDto
{
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set; }
    public ICollection<LaborCost>? ListLaborCosts { get; set; }
    public bool Reviewed { get; set; } = false;
}