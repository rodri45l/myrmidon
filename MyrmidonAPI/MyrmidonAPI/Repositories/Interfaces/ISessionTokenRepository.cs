using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface ISessionTokenRepository
{
    public Task<Result> StoreSessionToken(SessionToken sessionToken);
    public Task<ServiceResponse<User>> CheckSessionToken(string sessionToken);
    public Task<Result> DeleteSessionToken(string sessionId);
    public string CreateSessionCacheAsync(string userId);
    public string GetUserIdBySessionCacheAsync(string sessionId);
    public void RemoveCacheSessionId(string sessionId);
}