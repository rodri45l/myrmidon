using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;
using Tension = MyrmidonAPI.Models.Tension;

namespace MyrmidonAPI.Repositories;

public class TensionRepository : ITensionRepository
{

    private readonly MyrmidonContext _myrmidonContext;
    public TensionRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    async Task<ServiceResponse<Tension>> ITensionRepository.GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<Tension>();
        var tension = await _myrmidonContext.Tensions.FindAsync(id);

        if (tension == null) serviceResponse.Success = false;
        else serviceResponse.Data = tension;
        
        return serviceResponse;
    }
    

    public async Task<ServiceResponse<IEnumerable<Tension>>> GetAllAsync(Guid userId)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Tension>>();
        var tensions = await _myrmidonContext.Tensions.Where(t => t.Id == userId).ToListAsync();
        if (tensions.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = tensions;
        return serviceResponse;
    }

    public async Task<Result> AddAsync(Tension tension)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Tensions.AddAsync(tension);
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

    public async Task<Result>  UpdateAsync(Tension tension)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Tensions.Update(tension);
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

    public async Task<Result>  DeleteAsync(Tension tension)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Tensions.Remove(tension);
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