using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class TherapistRepository : ITherapistRepository
{

    private readonly MyrmidonContext _myrmidonContext;

    public TherapistRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<Therapist>> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<Therapist>();
        var therapist = await _myrmidonContext.Therapists.FindAsync(id);

        if (therapist == null) serviceResponse.Success = false;
        else serviceResponse.Data = therapist;
        
        return serviceResponse;
       
    }

    public async Task<ServiceResponse<IEnumerable<Therapist>>> GetAllByUserIdAsync(Guid userId)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Therapist>>();
        var therapists = await _myrmidonContext.Therapists.Where(t => t.Id == userId).ToListAsync();

        if (therapists.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = therapists;
        
        return serviceResponse;
    }

    public async Task<Result> AddAsync(Therapist therapist)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Therapists.AddAsync(therapist);
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

    public async Task<Result> UpdateAsync(Therapist therapist)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Therapists.Update(therapist);
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

    public async Task<Result> DeleteAsync(Therapist therapist)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Therapists.Remove(therapist);
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