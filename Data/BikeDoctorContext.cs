namespace BikeDoctor.Data;

using Microsoft.EntityFrameworkCore;
using BikeDoctor.Models;

public class BikeDoctorContext : DbContext
{
    public BikeDoctorContext(DbContextOptions<BikeDoctorContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Staff> Staffs { get; set; }
}