namespace MedApp.Domain.Models;

public record Address (
    string Street,
    string City,
    string State,
    string ZipCode,
    bool IsMailingAddress = false
); 