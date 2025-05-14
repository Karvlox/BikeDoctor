namespace BikeDoctor.Data;

using System.Text.Json;
using BikeDoctor.Models;
using Microsoft.EntityFrameworkCore;

public class BikeDoctorContext : DbContext
{
    public BikeDoctorContext(DbContextOptions<BikeDoctorContext> options)
        : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Reception> Receptions { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<SpareParts> SpareParts { get; set; }
    public DbSet<CostApproval> CostApprovals { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<QualityControl> QualityControls { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<FlowAttention> FlowAttentions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Client
        modelBuilder.Entity<Client>().HasKey(c => c.CI);

        // Motorcycle
        modelBuilder.Entity<Motorcycle>().HasKey(m => m.Id);
        modelBuilder
            .Entity<Motorcycle>()
            .HasOne(m => m.Client)
            .WithMany(c => c.Motorcycles)
            .HasForeignKey(m => m.ClientCI)
            .HasPrincipalKey(c => c.CI)
            .OnDelete(DeleteBehavior.Cascade);

        // Reception
        modelBuilder.Entity<Reception>().HasKey(r => r.Id);
        modelBuilder
            .Entity<Reception>()
            .Property(r => r.Reasons)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        modelBuilder
            .Entity<Reception>()
            .Property(r => r.Images)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        // Diagnosis
        modelBuilder.Entity<Diagnosis>().HasKey(d => d.Id);
        modelBuilder
            .Entity<Diagnosis>()
            .Property(d => d.ListDiagnostics)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v =>
                    JsonSerializer.Deserialize<ICollection<Diagnostic>>(
                        v,
                        new JsonSerializerOptions()
                    ) ?? new List<Diagnostic>()
            );

        // SpareParts
        modelBuilder.Entity<SpareParts>().HasKey(s => s.Id);
        modelBuilder
            .Entity<SpareParts>()
            .Property(s => s.ListSpareParts)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v =>
                    JsonSerializer.Deserialize<ICollection<SparePart>>(
                        v,
                        new JsonSerializerOptions()
                    ) ?? new List<SparePart>()
            );

        // CostApproval
        modelBuilder.Entity<CostApproval>().HasKey(ca => ca.Id);
        modelBuilder
            .Entity<CostApproval>()
            .Property(ca => ca.ListLaborCosts)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v =>
                    JsonSerializer.Deserialize<ICollection<LaborCost>>(
                        v,
                        new JsonSerializerOptions()
                    ) ?? new List<LaborCost>()
            );

        // Repair
        modelBuilder.Entity<Repair>().HasKey(r => r.Id);
        modelBuilder
            .Entity<Repair>()
            .Property(r => r.ListReparations)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v =>
                    JsonSerializer.Deserialize<ICollection<Reparation>>(
                        v,
                        new JsonSerializerOptions()
                    ) ?? new List<Reparation>()
            );

        // QualityControl
        modelBuilder.Entity<QualityControl>().HasKey(q => q.Id);
        modelBuilder
            .Entity<QualityControl>()
            .Property(q => q.ListControls)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v =>
                    JsonSerializer.Deserialize<ICollection<Control>>(v, new JsonSerializerOptions())
                    ?? new List<Control>()
            );

        // Delivery
        modelBuilder.Entity<Delivery>().HasKey(d => d.Id);

        // FlowAttention
        modelBuilder.Entity<FlowAttention>().HasKey(fa => fa.Id);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<Reception>()
            .WithMany()
            .HasForeignKey(fa => fa.ReceptionID);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<Diagnosis>()
            .WithMany()
            .HasForeignKey(fa => fa.DiagnosisID);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<SpareParts>()
            .WithMany()
            .HasForeignKey(fa => fa.SparePartsID);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<CostApproval>()
            .WithMany()
            .HasForeignKey(fa => fa.CostApprovalID);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<Repair>()
            .WithMany()
            .HasForeignKey(fa => fa.RepairID);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<QualityControl>()
            .WithMany()
            .HasForeignKey(fa => fa.QualityControlID);
        modelBuilder
            .Entity<FlowAttention>()
            .HasOne<Delivery>()
            .WithMany()
            .HasForeignKey(fa => fa.DeliveryID);

        // Reception for searching
        modelBuilder.Entity<Reception>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<Reception>().HasIndex(r => r.MotorcycleLicensePlate);

        // Diagnosis for searching
        modelBuilder.Entity<Diagnosis>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<Diagnosis>().HasIndex(r => r.MotorcycleLicensePlate);

        // SpareParts for searching
        modelBuilder.Entity<SpareParts>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<SpareParts>().HasIndex(r => r.MotorcycleLicensePlate);

        // CostApprovals for searching
        modelBuilder.Entity<CostApproval>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<CostApproval>().HasIndex(r => r.MotorcycleLicensePlate);

        // Repair for searching
        modelBuilder.Entity<Repair>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<Repair>().HasIndex(r => r.MotorcycleLicensePlate);

        // QualityControl for searching
        modelBuilder.Entity<QualityControl>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<QualityControl>().HasIndex(r => r.MotorcycleLicensePlate);

        // Delivery for searching
        modelBuilder.Entity<Delivery>().HasIndex(r => r.ClientCI);
        modelBuilder.Entity<Delivery>().HasIndex(r => r.MotorcycleLicensePlate);
    }
}
