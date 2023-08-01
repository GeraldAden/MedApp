namespace MedApp.Application;

using MedApp.Application.Abstractions.Services;
using MedApp.Application.Abstractions.Models;
using MedApp.Domain.Services;
using MedApp.Domain.Entities;

public interface IApplication
{
    public Task<User?> AuthenticateUserAsync(string username, string password);
    public Task AddPatientsAsync();
    public Task DisplayPatientsAsync();
}

public class Application : IApplication
{
    public Application(IUserService userService, IPatientService patientService)
    {
        _userService = userService;
        _patientService = patientService;
    }

    public async Task<User?> AuthenticateUserAsync(string username, string password)
    {
        return await _userService.AuthenticateUserAsync(username, password);
    }

    public async Task AddPatientsAsync()
    {

        var patient1 = new PatientBuilder()
            .WithFirstName("John")
            .WithLastName("Doe")
            .WithDateOfBirth(new DateTime(1990, 1, 1))
            .WithEmail("johndoe@site.com")
            .IsSmoker(false)
            .HasCancer(false)
            .HasDiabetes(false)
            .WithAddresses(new List<Address> {
                new Address ("123 Main St", "Anytown", "Anystate", "12345")
            }).Build();

        var patient2 = new PatientBuilder()
            .WithFirstName("Jane")
            .WithLastName("Doe")
            .WithDateOfBirth(new DateTime(1970, 1, 1))
            .WithEmail("janedoe@site.com")
            .IsSmoker(false)
            .HasCancer(true)
            .HasDiabetes(false)
            .WithAddresses(new List<Address> {
                new Address ("123 Main St", "Anytown", "Anystate", "12345", true)
            }).Build();

        await _patientService.AddPatientAsync(patient1);

        await _patientService.AddPatientAsync(patient2);
    }

    public async Task DisplayPatientsAsync()
    {
        var patients = await _patientService.GetPatientsAsync();

        foreach (var patient in patients)
        {
            Console.WriteLine($"Patient: {patient.FirstName} {patient.LastName}");
            foreach (var address in patient.Addresses)
            {
                Console.WriteLine($"Address: {address.Street}, {address.City}, {address.State} {address.ZipCode}");
            }
        }
    }

    public IUserService _userService { get; }
    public IPatientService _patientService { get; }
}
