using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MedApp.Domain.Data.Models;
using MedApp.Domain.Data.Builders;
using MedApp.Domain.Configuration;
using MedApp.Domain.Services;
using MedApp.Security.Configuration;
using MedApp.Security.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        ConfigureAppConfiguration(hostingContext, config);
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        ConfigureLogging(hostingContext, loggerConfiguration);
    })
    .ConfigureServices((hostContext, services) =>
    {
        DomainConfiguration.ConfigureDomain(hostContext, services);
        SecurityConfiguration.ConfigureSecurity(hostContext, services);
        ConfigureServices(hostContext, services);
    })
    .Build();

await RunApp(host.Services);

await host.RunAsync();

async Task RunApp(IServiceProvider services)
{
    Log.Debug($"Environment: {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}");
    Log.Debug($"ConnectionString: {Environment.GetEnvironmentVariable("ConnectionStrings__MedDb")}");

    using var scope = services.CreateScope();

    var authenticated = false;

    while (!authenticated)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        if (String.IsNullOrEmpty(username))
            continue;

        Console.Write("Enter password: ");
        var password = ReadPassword();
        if (String.IsNullOrEmpty(password))
            continue;

        authenticated = await AuthenticateUser(scope, username, password);
    }

    await AddPatients(scope);

    await DisplayPatients(scope);

    // TryCriteriaMatching(services);
}

void ConfigureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder config)
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddJsonFile($"appSettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    config.AddEnvironmentVariables();
}

void ConfigureLogging(HostBuilderContext hostContext, LoggerConfiguration loggerConfiguration)
{
    loggerConfiguration
        .ReadFrom.Configuration(hostContext.Configuration)
        .Enrich.FromLogContext();
}

void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
{
    services.AddScoped<IPatientService, PatientService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPatientService, PatientService>();
}

async Task<bool> AuthenticateUser(IServiceScope scope, string username, string password)
{
    Log.Debug("Authenticating user");

    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

    var user = await userService.GetAuthenticatedUserAsync(username, password);

    if (user != null)
    {
        Console.WriteLine($"Welcome {user.FirstName} {user.LastName}");
        return true;
    }
    else
    {
        Console.WriteLine("Invalid username or password");
        return false;
    }
}

async Task AddPatients(IServiceScope scope)
{
    Log.Debug("Adding patients");

    var patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();

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
        })
        .Build();

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
        })
        .Build();

        Log.Debug("Adding patient 1");
        await patientService.AddPatientAsync(patient1);

        Log.Information("Added patient 2");
        await patientService.AddPatientAsync(patient2);
}

async Task DisplayPatients(IServiceScope scope)
{
    Log.Debug("Displaying patients");

    var patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();

    var patients = await patientService.GetPatientsAsync();

    foreach (var patient in patients)
    {
        Console.WriteLine($"Patient: {patient.FirstName} {patient.LastName}");
        foreach (var address in patient.Addresses)
        {
            Console.WriteLine($"Address: {address.Street}, {address.City}, {address.State} {address.ZipCode}");
        }
    }
}

string ReadPassword()
{
    var password = new StringBuilder();

    while (true)
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter)
            break;
        password.Append(key.KeyChar);
    }
    return password.ToString();
}

// void TryCriteriaMatching()
// {
//     var patient1 = new PatientBuilder()
//         .WithDateOfBirth(new DateTime(1990, 1, 1))
//         .IsSmoker(false)
//         .HasCancer(false)
//         .HasDiabetes(false)
//         .WithAddresses(new List<Address> {
//             new Address ( "123 Main St", "Anytown", "Anystate", "12345")
//         })
//         .Build();

//     Console.WriteLine($"Patient 1 matches: {(PatientMatch.IsPatientMatch(patient1,
//         new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");

//     var patient2 = new PatientBuilder()
//         .WithDateOfBirth(new DateTime(1970, 1, 1))
//         .IsSmoker(false)
//         .HasCancer(true)
//         .HasDiabetes(false)
//         .WithAddresses(new List<Address> {
//             new Address ("123 Main St", "Anytown", "Anystate", "12345")
//         })
//         .Build();

//     Console.WriteLine($"Patient 2 matches: {(PatientMatch.IsPatientMatch(patient2,
//         new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");
// }

