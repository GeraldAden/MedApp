namespace MedApp.Repositories.Implementations;

using MedApp.Repositories.Interfaces;
using MedApp.Infrastructure.Database;
using MedApp.Infrastructure.Database.Entities;

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