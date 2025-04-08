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
    public DbSet<Reception> Receptions { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<SpareParts> SpareParts { get; set; }
    public DbSet<CostApproval> CostApprovals { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<QualityControl> QualityControls { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }


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

        // Reception
        modelBuilder.Entity<Reception>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<Reception>()
            .Property(r => r.Reasons)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList());
        modelBuilder.Entity<Reception>()
            .Property(r => r.Images)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList());

        // Diagnosis
        modelBuilder.Entity<Diagnosis>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Diagnosis>()
            .OwnsMany(d => d.ListDiagnostics, a =>
            {
                a.WithOwner().HasForeignKey("DiagnosisId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

        // SpareParts
        modelBuilder.Entity<SpareParts>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<SpareParts>()
            .OwnsMany(s => s.ListSpareParts, a =>
            {
                a.WithOwner().HasForeignKey("SparePartsId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

        // CostApproval
        modelBuilder.Entity<CostApproval>()
            .HasKey(ca => ca.Id);
        modelBuilder.Entity<CostApproval>()
            .OwnsMany(ca => ca.ListLaborCosts, a =>
            {
                a.WithOwner().HasForeignKey("CostApprovalId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

        // Repair
        modelBuilder.Entity<Repair>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<Repair>()
            .OwnsMany(r => r.ListReparations, a =>
            {
                a.WithOwner().HasForeignKey("RepairId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

        // QualityControl
        modelBuilder.Entity<QualityControl>()
            .HasKey(q => q.Id);
        modelBuilder.Entity<QualityControl>()
            .OwnsMany(q => q.ListControls, a =>
            {
                a.WithOwner().HasForeignKey("QualityControlId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });

        // Delivery
        modelBuilder.Entity<Delivery>()
            .HasKey(d => d.Id);
    }
}