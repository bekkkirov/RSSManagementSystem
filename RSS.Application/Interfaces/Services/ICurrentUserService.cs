namespace RSS.Application.Interfaces.Services;

public interface ICurrentUserService
{
    public int GetId();

    public string GetUserName();
}