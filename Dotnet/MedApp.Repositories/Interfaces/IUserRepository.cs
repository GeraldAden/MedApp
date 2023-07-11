namespace MedApp.Repositories.Interfaces;

using MedApp.Domain.Data.Models;

public interface IUserRepository
{
    // Task<IEnumerable<User>> GetUsersAsync();
    Task AddUserAsync(User user);
    Task<User> GetUserAsync(string username);
//     Task<bool> UpdatePatientAsync(Patient patient);
//     Task<bool> DeletePatientAsync(string id);
}
