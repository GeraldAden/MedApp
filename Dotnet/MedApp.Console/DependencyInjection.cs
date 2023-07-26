namespace MedApp.Console;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MedApp.Domain.Services;
using MedApp.Infrastructure.Security;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPatientService, PatientService>();
    }
}