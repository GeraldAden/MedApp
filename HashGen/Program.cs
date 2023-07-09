using MedApp.Security.Services;

if (args.Count() == 0)
{
    Console.WriteLine("Usage: HashGen.exe <string>");
    return;
}

var password = args[0];

var (hashedPassword, salt) = AuthenticationService.GenerateHashAndSalt(password);

Console.WriteLine($"Hashed password: {hashedPassword}");
Console.WriteLine($"Salt: {salt}");
