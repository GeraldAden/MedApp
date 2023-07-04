using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Patient
{
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public DateTime DateOfBirth {get; set;}
    public bool IsSmoker {get; set;}
    public bool HasCancer {get; set;}
    public bool HasDiabetes {get; set;}
    public ICollection<Address> Addresses {get; set;}
}

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(p => p.DateOfBirth)
            .HasColumnType("date");
    }
}