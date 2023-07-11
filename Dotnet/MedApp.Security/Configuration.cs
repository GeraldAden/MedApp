namespace MedApp.Security.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedApp.Security.Services;

public static class SecurityConfiguration
{
    public static void ConfigureSecurity(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}