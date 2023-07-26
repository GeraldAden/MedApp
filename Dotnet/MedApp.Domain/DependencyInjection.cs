namespace MedApp.Domain;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MedApp.Repositories.Interfaces;
using MedApp.Repositories.Implementations;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPatientRepository, PatientRepositoryEFPostgresImpl>();
        services.AddScoped<IUserRepository, UserRepositoryEFPostgresImpl>();
    }
}