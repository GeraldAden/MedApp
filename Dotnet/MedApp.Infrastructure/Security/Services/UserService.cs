namespace MedApp.Infrastructure.Security;

using Microsoft.EntityFrameworkCore;
using MedApp.Infrastructure.Database;
using MedApp.Infrastructure.Database.Entities;

public interface IUserService
{
    Task AddUserAsync(User user);
    Task<User?> AuthenticatedUserAsync(string username, string password);
}

public class UserService : IUserService
{

    public UserService(MedDbContext medDbContext, IAuthenticationService authenticationService)
    {
        _medDbContext = medDbContext;
        _authenticationService = authenticationService;
    }

    public async Task AddUserAsync(User user)
    {
        await _medDbContext.Users.AddAsync(user);
        await _medDbContext.SaveChangesAsync();
    }

    public async Task<User?> AuthenticatedUserAsync(string username, string password)
    {
        var user = await _medDbContext.Users
            .SingleOrDefaultAsync(user => user.Username == username);
        if (user is null)
            return null;

        #pragma warning disable CS8604
        var isPasswordValid = _authenticationService.IsPasswordValid(password, user.PasswordHash, user.PasswordSalt);
        #pragma warning disable CS8604
        
        if (!isPasswordValid)
            return null;

        return user;
    }

    private readonly MedDbContext _medDbContext;
    private readonly IAuthenticationService _authenticationService;
}