using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class JournalEntryRepository : IJournalEntryRepository
{
    public async Task<ServiceResponse<JournalEntry>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IEnumerable<JournalEntry>>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddAsync(JournalEntry journalEntry)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateAsync(JournalEntry journalEntry)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(JournalEntry journalEntry)
    {
        throw new NotImplementedException();
    }
}