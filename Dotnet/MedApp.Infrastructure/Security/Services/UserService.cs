namespace MedApp.Infrastructure.Security;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MedApp.Infrastructure.Database;
using Entities = MedApp.Infrastructure.Database.Entities;
using MedApp.Infrastructure.Security.Models;

public interface IUserService
{
    Task AddUserAsync(User user);
    Task<User?> AuthenticatedUserAsync(string username, string password);
}

public class UserService : IUserService
{

    public UserService(MedDbContext medDbContext, IAuthenticationService authenticationService, IMapper mapper)
    {
        _mapper = mapper;
        _medDbContext = medDbContext;
        _authenticationService = authenticationService;
    }

    public async Task AddUserAsync(User user)
    {
        var userEntity = _mapper.Map<Entities.User>(user);
        await _medDbContext.Users.AddAsync(userEntity);
        await _medDbContext.SaveChangesAsync();
    }

    public async Task<User?> AuthenticatedUserAsync(string username, string password)
    {
        var userEntity = await _medDbContext.Users
            .SingleOrDefaultAsync(user => user.Username == username);
        if (userEntity is null)
            return null;

        #pragma warning disable CS8604
        var isPasswordValid = _authenticationService.IsPasswordValid(password, userEntity.PasswordHash, userEntity.PasswordSalt);
        #pragma warning disable CS8604
        
        if (!isPasswordValid)
            return null;

        var user = _mapper.Map<User>(userEntity);
        return user;
    }

    private IMapper _mapper;
    private readonly MedDbContext _medDbContext;
    private readonly IAuthenticationService _authenticationService;
}