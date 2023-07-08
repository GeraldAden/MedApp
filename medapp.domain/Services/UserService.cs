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

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }

    private readonly IUserRepository _userRepository;
}