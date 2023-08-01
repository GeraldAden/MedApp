namespace MedApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using MedApp.Infrastructure.Persistence.Entities;

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

        modelBuilder.Entity<PatientEntity>()
            .ToTable("patients")
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<UserEntity>()
            .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<PatientEntity>()
            .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<AddressEntity>()
            .Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PatientEntity> Patients { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
}