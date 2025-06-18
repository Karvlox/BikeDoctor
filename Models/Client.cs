namespace BikeDoctor.Models;

using System.ComponentModel.DataAnnotations;

public class Client 
{
    [Required(ErrorMessage = "El CI es obligatorio")]
    public int CI { get; set;}
    public string Name { get; set;}
    public string LastName { get; set;}
    public int Age { get; set;}
    public int NumberPhone { get; set;}
    public string Gender { get; set;}

    public ICollection<Motorcycle>? Motorcycles { get; set; } = new List<Motorcycle>();
}