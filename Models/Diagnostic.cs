namespace BikeDoctor.Models;

public class Diagnostic
{    
    public string Error { get; set; }
    public string DetailOfError { get; set; }
    public string ServiceType  { get; set; }
    public int TimeSpent { get; set; } // minutes
}