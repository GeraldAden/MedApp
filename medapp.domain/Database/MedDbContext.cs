using Microsoft.EntityFrameworkCore;

public class MedDbContext : DbContext
{
    private readonly string _connectionString;

    public MedDbContext(DbContextOptions<MedDbContext> options, string connectionString) : base(options)
    {
        _connectionString = connectionString;
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connectionString)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("records");
        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId);
    }
}