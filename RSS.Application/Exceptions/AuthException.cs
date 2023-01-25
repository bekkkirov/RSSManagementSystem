namespace RSS.Application.Exceptions;

public class AuthException : Exception
{
    public AuthException()
    {
        
    }

    public AuthException(string message) : base(message)
    {
        
    }
}