namespace MedApp.Application;

using MedApp.Infrastructure.Security;
using MedApp.Infrastructure.Security.Models;
using MedApp.Domain.Services;
using MedApp.Domain.Models;

public interface IApplication
{
    public Task<User?> AuthenticatedUserAsync(string username, string password);
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

    public async Task<User?> AuthenticatedUserAsync(string username, string password)
    {
        return await _userService.AuthenticatedUserAsync(username, password);
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

    public Task DisplayPatientsAsync()
    {
        throw new NotImplementedException();
    }

    public IUserService _userService { get; }
    public IPatientService _patientService { get; }
}
