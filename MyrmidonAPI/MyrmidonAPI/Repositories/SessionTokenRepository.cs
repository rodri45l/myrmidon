using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;


public class SessionTokenRepository : ISessionTokenRepository
{
    private readonly MyrmidonContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public SessionTokenRepository(MyrmidonContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;

    }

    public async Task<Result> StoreSessionToken(SessionToken sessionToken)
    {
        try
        {
            await _dbContext.SessionTokens.AddAsync(sessionToken);
            await _dbContext.SaveChangesAsync();
            return new Result { Success = true};

        }
        catch (Exception ex)
        {
            return new Result { Success = false, Error = ex.Message };
        }
        
    }

    public async Task<ServiceResponse<User>> CheckSessionToken(string sessionId)
    {
        var serviceResponse = new ServiceResponse<User>();
        try
        {
            var sessionToken = await _dbContext.SessionTokens.FindAsync(sessionId);
            if (sessionToken == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid sessionId";
                return serviceResponse;
            }

            var user = await _dbContext.Users.FindAsync(sessionToken.UserId);
            serviceResponse.Data = user;

            return serviceResponse;

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Something went wrong";
            return serviceResponse;
        }
    }

    public async Task<Result> DeleteSessionToken(string sessionId)
    {
        try
        {
            var sessionToRemove = await _dbContext.SessionTokens.FindAsync(sessionId);
            _dbContext.SessionTokens.Remove(sessionToRemove!);
            await _dbContext.SaveChangesAsync();
            return new Result { Success = true};

        }
        catch (Exception ex)
        {
            return new Result { Success = false, Error = ex.Message };
        }

    }
    
    public string CreateSessionCacheAsync(string userId)
    {
        string sessionId = Guid.NewGuid().ToString();
        _memoryCache.Set(sessionId, userId, TimeSpan.FromMinutes(30));

        return sessionId;

    }

    public string GetUserIdBySessionCacheAsync(string sessionId)
    {
        if(_memoryCache.TryGetValue(sessionId, out string? userId))
        {
            return userId;
        }
        return null;
    }
    public void RemoveCacheSessionId(string sessionId)
    {
        _memoryCache.Remove(sessionId);
    }
}