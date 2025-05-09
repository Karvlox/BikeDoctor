namespace BikeDoctor.Models;

public class FlowAttention 
{
    public Guid Id { get; set; }
    public int ClientCI { get; set; }
    public string MotorcycleLicensePlate { get; set; }
    public int EmployeeCI { get; set;}
    public Guid ReceptionID { get; set; }
    public Guid DiagnosisID { get; set; }
    public Guid SparePartsID { get; set; }
    public Guid CostApprovalID { get; set; }
    public Guid RepairID { get; set; }
    public Guid QualityControlID { get; set; }
    public Guid DeliveryID { get; set; }
}