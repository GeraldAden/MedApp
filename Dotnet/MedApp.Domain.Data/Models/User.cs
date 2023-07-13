namespace MedApp.Domain.Data.Models;

public record User(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string PasswordSalt,
    string PasswordHash
);