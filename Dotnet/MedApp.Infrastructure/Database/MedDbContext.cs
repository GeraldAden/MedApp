using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        // modelBuilder.ApplyConfiguration(new UserConfiguration());
        // modelBuilder.ApplyConfiguration(new PatientConfiguration());
        // modelBuilder.ApplyConfiguration(new AddressConfiguration());

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

        // modelBuilder.Entity<Address>()
        //     .ToTable("addresses");
        // modelBuilder.Entity<Address>()
        //     .Property(a => a.Street).IsRequired();
        // modelBuilder.Entity<Address>()
        //     .Property(a => a.City).IsRequired();
        // modelBuilder.Entity<Address>()
        //     .Property(a => a.State).IsRequired();
        // modelBuilder.Entity<Address>()
        //     .Property(a => a.ZipCode).IsRequired();

        // modelBuilder.Entity<User>()
        //     .ToTable("users");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Address> Addresses { get; set; }
}

// public class UserConfiguration : IEntityTypeConfiguration<Address>
// {
//     public void Configure(EntityTypeBuilder<Address> builder)
//     {
//         builder.Property(u => u.CreatedAt)
//             .HasColumnType("timestamp")
//             .HasDefaultValueSql("GETUTCDATE()");

//         builder.Property(u => u.UpdatedAt)
//             .HasColumnType("timestamp");
//     }
// }

// public class PatientConfiguration : IEntityTypeConfiguration<Patient>
// {
//     public void Configure(EntityTypeBuilder<Patient> builder)
//     {
//         builder.Property(p => p.DateOfBirth)
//             .HasColumnType("date");

//         builder.Property(p => p.CreatedAt)
//             .HasColumnType("timestamp")
//             .HasDefaultValueSql("NOW()");

//         builder.Property(p => p.UpdatedAt)
//             .HasColumnType("timestamp");
//     }
// }

// public class AddressConfiguration : IEntityTypeConfiguration<User>
// {
//     public void Configure(EntityTypeBuilder<User> builder)
//     {
//         builder.Property(u => u.CreatedAt)
//             .HasColumnType("timestamp")
//             .HasDefaultValueSql("NOW()");

//         builder.Property(u => u.UpdatedAt)
//             .HasColumnType("timestamp");
//     }
// }