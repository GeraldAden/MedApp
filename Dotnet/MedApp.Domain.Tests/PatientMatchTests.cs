namespace MedApp.Domain.Tests;

using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using MedApp.Infrastructure.Database.Entities;

[TestFixture]
public class PatientMatchTests
{
    [Test]
    public void MatchAge_ReturnsTrue_WhenPatientAgeIsGreaterThanOrEqualToCriteriaAge()
    {
        // Arrange
        var patient = new Patient { DateOfBirth = new DateTime(1980, 1, 1) };
        var criteriaAge = 40;

        // Act
        var result = PatientMatch.MatchAge(patient, criteriaAge);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void MatchAge_ReturnsFalse_WhenPatientAgeIsLessThanCriteriaAge()
    {
        // Arrange
        var patient = new Patient { DateOfBirth = new DateTime(2010, 1, 1) };
        var criteriaAge = 21;

        // Act
        var result = PatientMatch.MatchAge(patient, criteriaAge);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void MatchAddress_ReturnsTrue_WhenPatientHasMatchingCity()
    {
        // Arrange
        var patient = new Patient
        {
            Addresses = new List<Address>
            {
                new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                new Address { Street = "456 Oak St", City = "Othertown", State = "CA", ZipCode = "67890" }
            }
        };
        var criteriaCity = "Anytown";

        // Act
        var result = PatientMatch.MatchCity(patient, criteriaCity);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void MatchAddress_ReturnsFalse_WhenPatientDoesNotHaveMatchingCity()
    {
        // Arrange
        var patient = new Patient
        {
            Addresses = new List<Address>
            {
                new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                new Address { Street = "456 Oak St", City = "Othertown", State = "CA", ZipCode = "67890" }
            }
        };
        var criteriaCity = "AnotherTown" ;

        // Act
        var result = PatientMatch.MatchCity(patient, criteriaCity);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Match_ReturnsTrue_WhenAllCriteriaMatch()
    {
        // Arrange
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1980, 1, 1),
            IsSmoker = false,
            HasCancer = false,
            HasDiabetes = true,
            Addresses = new List<Address>
            {
                new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" }
            }
        };
        var criteria = new PatientMatch.Criteria(40, false, "Anytown");

        // Act
        var result = PatientMatch.IsPatientMatch(patient, criteria);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Match_ReturnsFalse_WhenAnyCriteriaDoesNotMatch()
    {
        // Arrange
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1980, 1, 1),
            IsSmoker = false,
            HasCancer = false,
            HasDiabetes = true,
            Addresses = new List<Address>
            {
                new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" }
            }
        };
        var criteria = new PatientMatch.Criteria(40, false, "AnotherTown");

        // Act
        var result = PatientMatch.IsPatientMatch(patient, criteria);

        // Assert
        result.Should().BeFalse();
    }
}