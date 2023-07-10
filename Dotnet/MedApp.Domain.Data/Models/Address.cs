namespace MedApp.Domain.Data.Models;

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public bool IsMailingAddress { get; set; }
}