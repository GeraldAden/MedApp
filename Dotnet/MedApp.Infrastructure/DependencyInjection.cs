using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedApp.Infrastructure.Database;

namespace MedApp.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MedDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MedDb"));
        });
    }
}