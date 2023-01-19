using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class JournalEntryRepository : IJournalEntryRepository
{
    private readonly MyrmidonContext _myrmidonContext;

    public JournalEntryRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<JournalEntry>> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<JournalEntry>();
        var journalEntry = await _myrmidonContext.JournalEntries.FindAsync(id);

        if (journalEntry == null) serviceResponse.Success = false;
        else serviceResponse.Data = journalEntry;

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<JournalEntry>>> GetAllByUserIdAsync(Guid userId)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<JournalEntry>>();
        var journalEntries = await _myrmidonContext.JournalEntries.Where(t => t.Id == userId).ToListAsync();
        if (journalEntries.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = journalEntries;
        return serviceResponse;
    }

    public async Task<Result> AddAsync(JournalEntry journalEntry)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.JournalEntries.AddAsync(journalEntry);
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

    public async Task<Result> UpdateAsync(JournalEntry journalEntry)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.JournalEntries.Update(journalEntry);
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

    public async Task<Result> DeleteAsync(JournalEntry journalEntry)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.JournalEntries.Remove(journalEntry);
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