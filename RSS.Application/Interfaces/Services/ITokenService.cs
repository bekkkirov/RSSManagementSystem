namespace RSS.Application.Interfaces.Services;

public interface ITokenService
{
    public string GenerateToken(string userName, int userId);
}