using Microsoft.EntityFrameworkCore;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyrmidonContext _myrmidonContext;

    public UserRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<User>> GetByIdAsync(Guid id)
    {
        var serviceResponse = new ServiceResponse<User>();
        var user = await _myrmidonContext.Users.FindAsync(id);

        if (user == null) serviceResponse.Success = false;
        else serviceResponse.Data = user;

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<User>>> GetAllAsync()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<User>>();
        var users = await _myrmidonContext.Users.ToListAsync();
        if (!users.Any()) serviceResponse.Success = false;
        serviceResponse.Data = users;


        return serviceResponse;
    }

    public async Task<Result> AddAsync(User user)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Users.AddAsync(user);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(User user)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Users.Update(user);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
            result.Success = false;
        }

        return result;
    }

    public async Task<Result> DeleteAsync(User user)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Users.Remove(user);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
            result.Success = false;
        }

        return result;
    }
}