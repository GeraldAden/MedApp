namespace MedApp.Application;

using MedApp.Infrastructure.Security;
using MedApp.Infrastructure.Database.Entities;

public interface IApplication
{
    public Task<User?> AuthenticatedUserAsync(string username, string password);
}

public class Application : IApplication
{
    public Application(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User?> AuthenticatedUserAsync(string username, string password)
    {
        return await _userService.AuthenticatedUserAsync(username, password);
    }

    public IUserService _userService { get; }
}
