using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var optionsBuilder = new DbContextOptionsBuilder<MedDbContext>();
var dbContext = new MedDbContext(optionsBuilder.Options, configuration.GetConnectionString("MedDb"));

var patients = dbContext.Patients
    .Include(p => p.Addresses)
    .ToList();

foreach (var patient in patients)
{
    Log.Information($"Patient: {patient.FirstName} {patient.LastName}");
    foreach (var address in patient.Addresses)
    {
        Log.Information($"Address: {address.Street}, {address.City}, {address.State} {address.ZipCode}");
    }
}

var patient1 = new PatientBuilder()
    .WithDateOfBirth(new DateTime(1990, 1, 1))
    .IsSmoker(false)
    .HasCancer(false)
    .HasDiabetes(false)
    .WithAddresses(new List<Address> {
        new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
    })
    .Build();

var patient2 = new PatientBuilder()
    .WithDateOfBirth(new DateTime(1970, 1, 1))
    .IsSmoker(false)
    .HasCancer(true)
    .HasDiabetes(false)
    .WithAddresses(new List<Address> {
        new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
    })
    .Build();

Console.WriteLine($"Patient 1 matches: {(PatientMatch.IsPatientMatch(patient1,
    new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");

Console.WriteLine($"Patient 2 matches: {(PatientMatch.IsPatientMatch(patient2,
    new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");

