using Microsoft.AspNetCore.Http;
using RSS.Application.Interfaces.Services;
using System.Security.Claims;

namespace RSS.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal? _currentUser;

    public CurrentUserService(IHttpContextAccessor context)
    {
        _currentUser = context.HttpContext?.User;
    }

    public string GetUserName()
    {
        return _currentUser.FindFirstValue(ClaimTypes.Name);
    }

    public int GetId()
    {
        return int.Parse(_currentUser.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}