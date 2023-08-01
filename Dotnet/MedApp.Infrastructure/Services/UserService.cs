namespace MedApp.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MedApp.Application.Abstractions.Services;
using MedApp.Application.Abstractions.Models;
using MedApp.Infrastructure.Persistence;
using MedApp.Infrastructure.Persistence.Entities;

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
        var userEntity = _mapper.Map<UserEntity>(user);
        await _medDbContext.Users.AddAsync(userEntity);
        await _medDbContext.SaveChangesAsync();
    }

    public async Task<User?> AuthenticateUserAsync(string username, string password)
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