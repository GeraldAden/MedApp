public class UserRepositoryEFPostgresImpl : IUserRepository
{

    public UserRepositoryEFPostgresImpl(MedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
}