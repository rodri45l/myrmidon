using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.SessionTokenService;

public interface ISessionTokenService
{
    public Task<ServiceResponse<SessionToken>> CreateSessionToken(User user);
    public Task<ServiceResponse<bool>> DeleteSessionToken(string sessionId);
    
    public Task<ServiceResponse<User>> CheckSessionToken(string sessionId);

}