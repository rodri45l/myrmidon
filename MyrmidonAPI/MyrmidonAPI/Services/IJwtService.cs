namespace MyrmidonAPI.Services;

public interface IJwtService
{
    public string CreateTokenAsync(User user);
}