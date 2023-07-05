using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    // Task<IEnumerable<User>> GetUsersAsync();
    Task AddUserAsync(User patient);
//     Task<Patient> GetPatientAsync(string id);
//     Task<bool> UpdatePatientAsync(Patient patient);
//     Task<bool> DeletePatientAsync(string id);
}

public class UserService : IUserService
{

    public UserService(MedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task<IEnumerable<Patient>> GetPatientsAsync()
    // {
    //     return await _dbContext.Patients
    //         .Include(p => p.Addresses)
    //         .ToListAsync();
    // }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
}