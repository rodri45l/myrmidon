using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class MoodRepository : IMoodRepository
{
    private readonly MyrmidonContext _myrmidonContext;

    public MoodRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<Mood>> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<Mood>();
        var mood = await _myrmidonContext.Moods.FindAsync(id);

        if (mood == null) serviceResponse.Success = false;
        else serviceResponse.Data = mood;

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<Mood>>> GetAllByUserIdAsync(Guid userId)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Mood>>();
        var moods = await _myrmidonContext.Moods.Where(t => t.Id == userId).ToListAsync();
        if (moods.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = moods;
        return serviceResponse;
    }

    public async Task<Result> AddAsync(Mood mood)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Moods.AddAsync(mood);
            var state = _myrmidonContext.Entry(mood).State;
            if (state == EntityState.Added) {
                await _myrmidonContext.SaveChangesAsync();
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Error = "Mood not valid";

            }


        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(Mood mood)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Moods.Update(mood);
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

    public async Task<Result> DeleteAsync(Mood mood)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Moods.Remove(mood);
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
}