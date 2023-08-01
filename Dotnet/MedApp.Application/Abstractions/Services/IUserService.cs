namespace MedApp.Application.Abstractions.Services;

using MedApp.Application.Abstractions.Models;

public interface IUserService // implemented by Infrastructure
{
    Task AddUserAsync(User user);
    Task<User?> AuthenticateUserAsync(string username, string password);
}