namespace MedApp.Domain.Data.Models;

public record Patient(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Email,
    ICollection<Address> Addresses,
    bool IsSmoker = false,
    bool HasCancer = false,
    bool HasDiabetes = false
);