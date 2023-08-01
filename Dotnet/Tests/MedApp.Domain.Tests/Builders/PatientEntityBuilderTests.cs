namespace MedApp.Domain.Entities.Builders.Tests;

using FluentAssertions;
using MedApp.Domain.Entities;

[TestFixture]
public class PatientEntityBuilderTests
{
    [Test]
    public void Build_ReturnsPatientWithCorrectProperties()
    {
        // Arrange
        var builder = new PatientBuilder()
            .WithFirstName("John")
            .WithLastName("Doe")
            .WithEmail("johndoe@example.com")
            .WithDateOfBirth(new DateTime(1980, 1, 1))
            .IsSmoker(false)
            .HasCancer(false)
            .HasDiabetes(true)
            .WithAddresses(new List<Address>
                {
                    new Address ( "123 Main St", "Anytown", "CA", "12345" )
                }
            );

        // Act
        var patient = builder.Build();

        // Assert
        patient.FirstName.Should().Be("John");
        patient.LastName.Should().Be("Doe");
        patient.DateOfBirth.Should().Be(new DateTime(1980, 1, 1));
        patient.IsSmoker.Should().BeFalse();
        patient.HasCancer.Should().BeFalse();
        patient.HasDiabetes.Should().BeTrue();
        patient.Addresses.Should().HaveCount(1);
        patient.Addresses.First().Street.Should().Be("123 Main St");
        patient.Addresses.First().City.Should().Be("Anytown");
        patient.Addresses.First().State.Should().Be("CA");
        patient.Addresses.First().ZipCode.Should().Be("12345");
    }
}