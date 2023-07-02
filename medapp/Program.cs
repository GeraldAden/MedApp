using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var patient1 = new Patient.Builder()
    .WithAge(30)
    .IsSmoker(false)
    .HasCancer(false)
    .HasDiabetes(false)
    .WithAddress(new Address ("123 Main St","Anytown","Anystate","12345" ))
    .Build();

var patient2 = new Patient.Builder()
    .WithAge(51)
    .IsSmoker(false)
    .HasCancer(true)
    .HasDiabetes(false)
    .WithAddress(new Address ("123 Main St", "Anytown", "Anystate", "12345"))
    .Build();

Console.WriteLine($"Patient 1 matches: {(PatientMatch.IsMatchPatient(patient1,
    new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");

Console.WriteLine($"Patient 2 matches: {(PatientMatch.IsMatchPatient(patient2,
    new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");
