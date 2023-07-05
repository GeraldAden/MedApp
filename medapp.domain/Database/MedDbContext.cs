using Microsoft.EntityFrameworkCore;

public class MedDbContext : DbContext
{
    public MedDbContext(DbContextOptions<MedDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("records");

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId);
    }
}