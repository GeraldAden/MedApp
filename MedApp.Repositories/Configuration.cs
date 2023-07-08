using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedApp.Infrastructure.Configuration;

namespace MedApp.Repositories.Configuration;

public static class RepositoriesConfiguration
{
    public static void ConfigureRepositories(HostBuilderContext hostContext, IServiceCollection services)
    {
        InfrastructureConfiguration.Configure(hostContext, services);
        services.AddScoped<IPatientRepository, PatientRepositryEFPostgresImpl>();
        services.AddScoped<IUserRepository, UserRepositoryEFPostgresImpl>();
    }
}