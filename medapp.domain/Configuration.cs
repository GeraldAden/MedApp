using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedApp.Repositories.Configuration;

namespace MedApp.Domain.Configuration;

public static class DomainConfiguration
{
    public static void ConfigureDomain(HostBuilderContext hostContext, IServiceCollection services)
    {
        RepositoriesConfiguration.ConfigureRepositories(hostContext, services);
        services.AddScoped<IPatientRepository, PatientRepositryEFPostgresImpl>();
        services.AddScoped<IUserRepository, UserRepositoryEFPostgresImpl>();
    }
}