namespace MedApp.Repositories.Implementations;

using AutoMapper;
using MedApp.Repositories.Interfaces;
using MedApp.Infrastructure.Database;
using MedApp.Infrastructure.Database.Entities;

public class UserRepositoryEFPostgresImpl : IUserRepository
{

    public UserRepositoryEFPostgresImpl(MedDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
    private readonly IMapper _mapper;
}