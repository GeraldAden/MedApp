using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedApp.Infrastructure.Database;

namespace MedApp.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static void Configure(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddDbContext<MedDbContext>(options =>
        {
            options.UseNpgsql(hostContext.Configuration.GetConnectionString("MedDb"));
        });
    }
}