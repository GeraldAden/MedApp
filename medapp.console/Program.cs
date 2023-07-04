using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
    ConfigureDatabase(hostContext, services);
    
    services.AddScoped<IPatientService, PatientService>();
}

void ConfigureDatabase(HostBuilderContext hostContext, IServiceCollection services)
{
    services.AddDbContext<MedDbContext>(options =>
    {
        options.UseNpgsql(hostContext.Configuration.GetConnectionString("MedDb"));
    });
    
    var databaseSettings = new DatabaseSettings();
    hostContext.Configuration.GetSection("Database").Bind(databaseSettings);
    services.AddSingleton(databaseSettings);
}

Log.Information($"Environment: {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}");

await AddPatients();

await DisplayPatients();

// TryCriteriaMatching();

async Task AddPatients()
{
    Log.Debug("Adding patients");

    var patient1 = new PatientBuilder()
        .WithFirstName("John")
        .WithLastName("Doe")
        .WithDateOfBirth(new DateTime(1990, 1, 1))
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
        .IsSmoker(false)
        .HasCancer(true)
        .HasDiabetes(false)
        .WithAddresses(new List<Address> {
            new Address { Street="123 Main St", City = "Anytown", State = "Anystate", ZipCode = "12345"}
        })
        .Build();
    
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var patientService = services.GetRequiredService<IPatientService>();

        Log.Debug("Adding patient 1");
        await patientService.AddPatientAsync(patient1);

        Log.Information("Added patient 2");
        await patientService.AddPatientAsync(patient2);
    }
}

async Task DisplayPatients()
{
    Log.Debug("Displaying patients");

    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var patientService = services.GetRequiredService<IPatientService>();
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

