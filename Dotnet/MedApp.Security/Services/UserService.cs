namespace MedApp.Security.Services;

using MedApp.Domain.Data.Models;
using MedApp.Repositories.Interfaces;

public interface IUserService
{
    // Task<IEnumerable<User>> GetUsersAsync();
    Task AddUserAsync(User user);
    Task<User?> GetAuthenticatedUserAsync(string username, string password);
//     Task<bool> UpdatePatientAsync(Patient patient);
//     Task<bool> DeletePatientAsync(string id);
}

public class UserService : IUserService
{

    public UserService(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }

    public async Task<User?> GetAuthenticatedUserAsync(string username, string password)
    {
        var user = await _userRepository.GetUserAsync(username);
        if (user == null)
            return null;

        var isPasswordValid = _authenticationService.IsPasswordValid(password, user.PasswordHash, user.PasswordSalt);
        if (!isPasswordValid)
            return null;

        return user;
    }

    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;
}