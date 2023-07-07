using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedApp.Infrastructure.Database;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddDbContext<MedDbContext>(options =>
        {
            options.UseNpgsql(hostContext.Configuration.GetConnectionString("MedDb"));
        });
        
        var databaseSettings = new DatabaseSettings();
        hostContext.Configuration.GetSection("DatabaseSettings").Bind(databaseSettings);
        services.AddSingleton(databaseSettings);
    }
}