namespace MedApp.Domain.Tests.Builders;

using FluentAssertions;
using MedApp.Domain.Data.Models;
using MedApp.Domain.Data.Builders;

[TestFixture]
public class PatientBuilderTests
{
    [Test]
    public void Build_ReturnsPatientWithCorrectProperties()
    {
        // Arrange
        var builder = new PatientBuilder()
            .WithFirstName("John")
            .WithLastName("Doe")
            .WithDateOfBirth(new DateTime(1980, 1, 1))
            .IsSmoker(false)
            .HasCancer(false)
            .HasDiabetes(true)
            .WithAddresses(new List<Address> {
                new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" }
            });

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