using Microsoft.AspNetCore.Identity;
using RSS.Application.Exceptions;
using RSS.Application.Interfaces.Repositories;
using RSS.Application.Interfaces.Services;
using RSS.Application.Models;
using RSS.Domain.Entities;
using RSS.Infrastructure.Identity.Entities;

namespace RSS.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly SignInManager<UserIdentity> _signInManager;

    public AuthService(IUnitOfWork unitOfWork,
                                 ITokenService tokenService,
                                 UserManager<UserIdentity> userManager,
                                 SignInManager<UserIdentity> signInManager)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<string> SignInAsync(AuthDto authData)
    {
        var userIdentity = await _userManager.FindByNameAsync(authData.UserName);

        if (userIdentity is null)
        {
            throw new AuthException("User with specified username doesn't exist.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(userIdentity, authData.Password, false);

        if (!result.Succeeded)
        {
            throw new AuthException("Invalid password.");
        }

        var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userIdentity.UserName);

        return _tokenService.GenerateToken(user.UserName, user.Id);
    }

    public async Task<string> SignUpAsync(AuthDto authData)
    {
        var identityToAdd = new UserIdentity() { UserName = authData.UserName };

        var result = await _userManager.CreateAsync(identityToAdd, authData.Password);

        if (!result.Succeeded)
        {
            throw new AuthException(result.Errors.FirstOrDefault()?.Description ?? "Error while creating new user.");
        }

        var userToAdd = new User() {UserName = authData.UserName};

        _unitOfWork.UserRepository.Add(userToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _tokenService.GenerateToken(userToAdd.UserName, userToAdd.Id);
    }
}