namespace MedApp.Application.Services;

using MedApp.Application.Models;

public interface IUserService // implemented by Infrastructure
{
    Task AddUserAsync(User user);
    Task<User?> AuthenticateUserAsync(string username, string password);
}