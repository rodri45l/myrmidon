using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.SessionTokenService;

public class SessionTokenService : ISessionTokenService
{
    private readonly ISessionTokenRepository _sessionTokenRepository;

    public SessionTokenService(ISessionTokenRepository sessionTokenRepository)
    {
        _sessionTokenRepository = sessionTokenRepository;
    }

    public virtual async Task<ServiceResponse<SessionToken>> CreateSessionToken(User user)
    {
        var sessionToken = new SessionToken();
        var serviceResponse = new ServiceResponse<SessionToken>();
        // Create Session token and load into cache
        sessionToken.TokenId = Guid.Parse(_sessionTokenRepository.CreateSessionCacheAsync(user.Id.ToString()));
        sessionToken.UserId = user.Id;
        sessionToken.ExpirationTime = DateTime.UtcNow.AddDays(30);
        // Store session token in RDB
        var result = await _sessionTokenRepository.StoreSessionToken(sessionToken);
        if (result.Success)
        {
            serviceResponse.Data = sessionToken;
            return serviceResponse;
        }

        serviceResponse.Success = false;
        serviceResponse.Message = "An error ocurred";
        return serviceResponse;
    }


    public virtual async Task<ServiceResponse<bool>> DeleteSessionToken(string sessionId)
    {
        var serviceResponse = new ServiceResponse<bool>();
        // Delete from cache 
        _sessionTokenRepository.RemoveCacheSessionId(sessionId);
        // Delete from rdb
        var result = await _sessionTokenRepository.DeleteSessionToken(sessionId);
        //return result
        serviceResponse.Success = result.Success;
        return serviceResponse;
    }

    public virtual async Task<ServiceResponse<User>> CheckSessionToken(string sessionId)
    {
        var cacheSessionId =await _sessionTokenRepository.GetUserBySessionCacheAsync(sessionId);
        if (cacheSessionId.Success)
        {
            return cacheSessionId;
        }


        return await _sessionTokenRepository.CheckSessionToken(sessionId);
    }
}