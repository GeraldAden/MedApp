using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MedApp.Domain;
using MedApp.Domain.Buiiders;
using MedApp.Domain.Configuration;
using MedApp.Domain.Services;
using MedApp.Infrastructure.Database.Entities;

Log.Information($"Environment: {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}");

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
        ConfigureServices(hostContext, services);
    })
    .Build();

await RunApp(host.Services);

await host.RunAsync();

async Task RunApp(IServiceProvider services)
{
    await AddUsers(services);

    await AddPatients(services);

    await DisplayPatients(services);

    // TryCriteriaMatching(services);
}

void ConfigureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder config)
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddJsonFile($"appSettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
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

async Task AddUsers(IServiceProvider services)
{
    Log.Debug("Adding users");

    using var scope = services.CreateScope();

    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

    var user = new User {
        FirstName = "John",
        LastName = "Doe",
        Username = "johndoe",
        Email = "johndoe@sie.com",
        Salt = "cZv0TStO5Tgj41leZg+vLCGg75AL/KlL+lV15GbQ098=",
        HashedPassword = "QKfLAcZdfFc1vYRGyadc65vshqY8Feoh+V4BMu+bhZflJL+s6K++z/FiIEe+3b7/EoP1lNExvjrJfU+M5Knojw=="
    };

    await userService.AddUserAsync(user);
}

async Task AddPatients(IServiceProvider services)
{
    Log.Debug("Adding patients");

    using var scope = services.CreateScope();

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
            new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
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
            new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
        })
        .Build();

        Log.Debug("Adding patient 1");
        await patientService.AddPatientAsync(patient1);

        Log.Information("Added patient 2");
        await patientService.AddPatientAsync(patient2);
}

async Task DisplayPatients(IServiceProvider services)
{
    Log.Debug("Displaying patients");

    using var scope = services.CreateScope();
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

void TryCriteriaMatching()
{
    var patient1 = new PatientBuilder()
        .WithDateOfBirth(new DateTime(1990, 1, 1))
        .IsSmoker(false)
        .HasCancer(false)
        .HasDiabetes(false)
        .WithAddresses(new List<Address> {
            new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
        })
        .Build();

    Console.WriteLine($"Patient 1 matches: {(PatientMatch.IsPatientMatch(patient1,
        new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");

    var patient2 = new PatientBuilder()
        .WithDateOfBirth(new DateTime(1970, 1, 1))
        .IsSmoker(false)
        .HasCancer(true)
        .HasDiabetes(false)
        .WithAddresses(new List<Address> {
            new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
        })
        .Build();

    Console.WriteLine($"Patient 2 matches: {(PatientMatch.IsPatientMatch(patient2,
        new PatientMatch.Criteria(51, true, "Anytown" )) ? "Yes" : "No")}");
}

