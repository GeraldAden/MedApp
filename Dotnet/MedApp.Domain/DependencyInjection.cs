namespace MedApp.Domain;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MedApp.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPatientRepository, PatientRepositoryEFPostgresImpl>();
        services.AddScoped<IPatientRepository, PatientRepositoryEFPostgresImpl>();
        services.AddAutoMapper(typeof(MappingProfile));
    }
}