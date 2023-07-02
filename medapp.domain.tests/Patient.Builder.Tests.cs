using FluentAssertions;

[TestFixture]
public class PatientMatchTests
{
    [Test]
    public void IsMatchPatient_ReturnsTrue_WhenAllCriteriaMatch()
    {
        // Arrange
        var patient = new Patient.Builder()
            .WithAge(50)
            .IsSmoker(false)
            .HasCancer(true)
            .HasDiabetes(false)
            .WithAddress(new Address (
                "123 Main St",
                "Anytown",
                "Anystate",
                "12345" ))
            .Build();

        var criteria = new PatientMatch.Criteria(50, true, "Anytown");

        // Act
        var result = PatientMatch.IsMatchPatient(patient, criteria);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsMatchPatient_ReturnsFalse_WhenAnyCriteriaDoesNotMatch()
    {
        // Arrange
        var patient = new Patient.Builder()
            .WithAge(50)
            .IsSmoker(false)
            .HasCancer(true)
            .HasDiabetes(false)
            .WithAddress(new Address (
                "123 Main St",
                "Anytown",
                "Anystate",
                "12345" ))
            .Build();

        var criteria = new PatientMatch.Criteria(50, false, "Othertown");

        // Act
        var result = PatientMatch.IsMatchPatient(patient, criteria);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void IsMatchPatient_ReturnsFalse_WhenAgeCriteriaDoesNotMatch()
    {
        // Arrange
        var patient = new Patient.Builder()
            .WithAge(50)
            .IsSmoker(false)
            .HasCancer(true)
            .HasDiabetes(false)
            .WithAddress(new Address (
                "123 Main St",
                "Anytown",
                "Anystate",
                "12345" ))
            .Build();

        var criteria = new PatientMatch.Criteria(60, true, "Anytown");

        // Act
        var result = PatientMatch.IsMatchPatient(patient, criteria);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void IsMatchPatient_ReturnsFalse_WhenCityCriteriaDoesNotMatch()
    {
        // Arrange
        var patient = new Patient.Builder()
            .WithAge(50)
            .IsSmoker(false)
            .HasCancer(true)
            .HasDiabetes(false)
            .WithAddress(new Address (
                "123 Main St",
                "Anytown",
                "Anystate",
                "12345" ))
            .Build();

        var criteria = new PatientMatch.Criteria(50, true, "Othertown");

        // Act
        var result = PatientMatch.IsMatchPatient(patient, criteria);

        // Assert
        result.Should().BeFalse();
    }
}