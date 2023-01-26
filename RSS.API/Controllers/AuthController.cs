using Microsoft.AspNetCore.Mvc;
using RSS.Application.Interfaces.Services;
using RSS.Application.Models;

namespace RSS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<string>> SignIn(AuthDto authData)
    {
        var token = await _authService.SignInAsync(authData);

        return Ok(token);
    }
    
    [HttpPost("sign-up")]
    public async Task<ActionResult<string>> SignUp(AuthDto authData)
    {
        var token = await _authService.SignUpAsync(authData);

        return Ok(token);
    }
}