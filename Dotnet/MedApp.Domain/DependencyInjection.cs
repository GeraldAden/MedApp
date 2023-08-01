namespace MedApp.Domain;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MedApp.Domain.Services;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPatientService, PatientService>();
    }
}