using RSS.Application.Models;

namespace RSS.Application.Interfaces.Services;

public interface IAuthService
{
    Task<string> SignInAsync(AuthDto authData);

    Task<string> SignUpAsync(AuthDto authData);
}