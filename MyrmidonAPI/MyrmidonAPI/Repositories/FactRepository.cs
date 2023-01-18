using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class FactRepository : IFactRepository
{
    private readonly MyrmidonContext _myrmidonContext;

    public FactRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<Fact>> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<Fact>();
        var fact = await _myrmidonContext.Facts.FindAsync(id);

        if (fact == null) serviceResponse.Success = false;
        else serviceResponse.Data = fact;

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<Fact>>> GetAllAsync()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Fact>>();
        var facts = await _myrmidonContext.Facts.ToListAsync();
        if (facts.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = facts;
        return serviceResponse;
    }

    public async Task<Result> AddAsync(Fact fact)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Facts.AddAsync(fact);
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

    public async Task<Result> UpdateAsync(Fact fact)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Facts.Update(fact);
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

    public async Task<Result> DeleteAsync(Fact fact)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Facts.Remove(fact);
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