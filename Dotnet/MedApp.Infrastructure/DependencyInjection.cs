using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedApp.Application.Services;
using MedApp.Domain.Repositories;
using MedApp.Infrastructure.Database;
using MedApp.Infrastructure.Services;
using MedApp.Infrastructure.Repositories;

namespace MedApp.Infrastructure;

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