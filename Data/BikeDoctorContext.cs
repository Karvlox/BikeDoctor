namespace BikeDoctor.Data;

using Microsoft.EntityFrameworkCore;
using BikeDoctor.Models;

public class BikeDoctorContext : DbContext
{
    public BikeDoctorContext(DbContextOptions<BikeDoctorContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasKey(c => c.CI);
    
        modelBuilder.Entity<Motorcycle>()
            .HasKey(m => m.Id);
        
        modelBuilder.Entity<Motorcycle>()
            .HasOne(m => m.Client)
            .WithMany(c => c.Motorcycles)
            .HasForeignKey(m => m.ClientCI)
            .HasPrincipalKey(c => c.CI)  
            .OnDelete(DeleteBehavior.Cascade);
    }
}