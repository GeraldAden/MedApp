namespace MedApp.Repositories.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedApp.Infrastructure.Configuration;
using MedApp.Repositories.Interfaces;
using MedApp.Repositories.Implementations;

public static class RepositoriesConfiguration
{
    public static void ConfigureRepositories(HostBuilderContext hostContext, IServiceCollection services)
    {
        InfrastructureConfiguration.Configure(hostContext, services);
        services.AddScoped<IPatientRepository, PatientRepositryEFPostgresImpl>();
        services.AddScoped<IUserRepository, UserRepositoryEFPostgresImpl>();
        services.AddAutoMapper(typeof(MappingProfile));
    }
}