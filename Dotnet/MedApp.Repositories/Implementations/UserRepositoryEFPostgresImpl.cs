namespace MedApp.Repositories.Implementations;

using Microsoft.EntityFrameworkCore;
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

    public async Task<User> GetUserAsync(string username)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return _mapper.Map<User>(userEntity);
    }

    private readonly MedDbContext _dbContext;
    private readonly IMapper _mapper;
}