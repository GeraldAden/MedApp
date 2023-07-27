using Microsoft.EntityFrameworkCore;
using MedApp.Infrastructure.Database.Entities;

namespace MedApp.Infrastructure.Database;

public class MedDbContext : DbContext
{
    public MedDbContext(DbContextOptions<MedDbContext> options) : base(options)
    {
       ChangeTracker.QueryTrackingBehavior =
       QueryTrackingBehavior.NoTracking;
       this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("records");

        modelBuilder.Entity<Patient>()
            .ToTable("patients")
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<User>()
            .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Patient>()
            .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Address>()
            .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Address> Addresses { get; set; }
}