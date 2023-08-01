namespace MedApp.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedApp.Application.Abstractions.Services;
using MedApp.Domain.Abstractions.Repositories;
using MedApp.Infrastructure.Services;
using MedApp.Infrastructure.Persistence;
using MedApp.Infrastructure.Persistence.Repositories;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MedDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MedDb"));
        });
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPatientRepository, PatientRepositoryEFPostgresImpl>();
        services.AddAutoMapper(typeof(MappingProfile));
    }
}