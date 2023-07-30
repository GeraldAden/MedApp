using Microsoft.Extensions.DependencyInjection;
using MedApp.Infrastructure.Services;

string passwordHash;
string passwordSalt;

if (args.Count() == 0)
{
    Console.WriteLine("Usage: HashGen.exe <string>");
    return;
}

var password = args[0];

var services = new ServiceCollection();
services.AddTransient<IAuthenticationService, AuthenticationService>();

var serviceProvider = services.BuildServiceProvider();
using (var scope = serviceProvider.CreateScope())
{
    var authenticationService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();

    (passwordHash, passwordSalt) = authenticationService.GenerateHashAndSalt(password);
}

Console.WriteLine($"Password Hash: {passwordHash}");
Console.WriteLine($"Password Salt: {passwordSalt}");
