namespace MedApp.Repositories.Interfaces;

using MedApp.Infrastructure.Database.Entities;

public interface IUserRepository
{
    // Task<IEnumerable<User>> GetUsersAsync();
    Task AddUserAsync(User patient);
//     Task<Patient> GetPatientAsync(string id);
//     Task<bool> UpdatePatientAsync(Patient patient);
//     Task<bool> DeletePatientAsync(string id);
}