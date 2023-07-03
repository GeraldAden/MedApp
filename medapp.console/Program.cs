using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddJsonFile($"appSettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<MedDbContext>(options =>
        {
            options.UseNpgsql(hostContext.Configuration.GetConnectionString("MedDb"));
        });
        
        var databaseSettings = new DatabaseSettings();
        hostContext.Configuration.GetSection("Database").Bind(databaseSettings);
        services.AddSingleton(databaseSettings);

        services.AddScoped<IPatientService, PatientService>();
    })
    .Build();

Log.Information($"Environment: {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}");

// TryCriteriaMatching();

DisplayPatients();

// using (var scope = host.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var patientService = services.GetRequiredService<IPatientService>();

//     var patients = await patientService.GetPatientsAsync();

//     foreach (var patient in patients)
//     {
//         Log.Information($"Patient: {patient.FirstName} {patient.LastName}");
//         foreach (var address in patient.Addresses)
//         {
//             Log.Information($"Address: {address.Street}, {address.City}, {address.State} {address.ZipCode}");
//         }
//     }
// }

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
}

async void DisplayPatients()
{
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

