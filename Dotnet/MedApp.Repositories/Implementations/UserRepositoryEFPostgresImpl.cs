namespace MedApp.Repositories.Implementations;

using AutoMapper;
using MedApp.Repositories.Interfaces;
using MedApp.Infrastructure.Database;
using Entities = MedApp.Infrastructure.Database.Entities;
using MedApp.Domain.Data.Models;

public class UserRepositoryEFPostgresImpl : IUserRepository
{

    public UserRepositoryEFPostgresImpl(MedDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task AddUserAsync(User user)
    {
        var userEntity = _mapper.Map<Entities.User>(user);
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
    private readonly IMapper _mapper;
}