using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSS.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RSS.Infrastructure.Options;

namespace RSS.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _tokenOptions;

    public TokenService(IOptions<JwtOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions;
    }

    public string GenerateToken(string userName, int userId)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userName)
        };

        var bytes = Encoding.UTF8.GetBytes(_tokenOptions.Value.Key);
        var key = new SymmetricSecurityKey(bytes);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(_tokenOptions.Value.ExpiresInDays),
            SigningCredentials = credentials,
            Issuer = _tokenOptions.Value.Issuer,
            Audience = _tokenOptions.Value.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}