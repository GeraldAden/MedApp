namespace MedApp.Security;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MedApp.Security.Services;

public static class DependencyInjection
{
    public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}