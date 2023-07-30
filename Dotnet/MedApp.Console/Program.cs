using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MedApp.Application;
using MedApp.Console;
using MedApp.Domain.Services;
using MedApp.Infrastructure;
using MedApp.Infrastructure.Security.Models;

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
        ConfigureServices(hostContext, services);
    })
    .Build();

await RunApp(host.Services);

await host.RunAsync();

async Task RunApp(IServiceProvider services)
{
    // Log.Debug($"Environment: {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}");

    using var scope = services.CreateScope();

    var application = scope.ServiceProvider.GetRequiredService<IApplication>();

    await AuthenticateUser(application);

    while (true)
    {
        Console.Write("Enter command (add, display, exit): ");
        var command = Console.ReadLine();

        if (String.IsNullOrEmpty(command))
            continue;

        if (command.ToLower() == "exit")
            break;

        if (command.ToLower() == "add")
        {
            await AddPatients(application);
        }

        if (command.ToLower() == "display")
        {
            await DisplayPatients(scope);
        }
    }

    // TryCriteriaMatching(services);

    Environment.Exit(0);
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
    services.AddInfrastructure(hostContext.Configuration);
    services.AddApplication(hostContext.Configuration);
}

async Task<User> AuthenticateUser(IApplication application)
{
    Log.Debug("Authenticating user");

    while (true)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        if (String.IsNullOrEmpty(username))
            continue;

        Console.Write("Enter password: ");
        var password = ReadPassword();
        if (String.IsNullOrEmpty(password))
            continue;

        var user = await application.AuthenticatedUserAsync(username, password);

        if (user is null)
        {
            Console.WriteLine("Invalid username or password");
            continue;
        }
        
        Console.WriteLine($"Welcome {user.FirstName} {user.LastName}");
        return user;
    }
}

async Task AddPatients(IApplication application)
{
    Log.Debug("Adding patients");

    await application.AddPatientsAsync();
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

