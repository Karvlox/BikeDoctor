namespace BikeDoctor.DTOs;

public class UpdateDeliveryDto
{
    public DateTime Date { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set; }
    public bool SurveyCompleted { get; set; }
    public bool Reviewed { get; set; } = false;
}