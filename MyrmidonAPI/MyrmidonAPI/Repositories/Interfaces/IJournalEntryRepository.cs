using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IJournalEntryRepository
{
    public Task<ServiceResponse<JournalEntry>> GetByIdAsync(int id);
    public Task<ServiceResponse<IEnumerable<JournalEntry>>> GetAllByUserIdAsync(Guid userId);
    public Task<Result>  AddAsync(JournalEntry journalEntry);
    public Task<Result>  UpdateAsync(JournalEntry journalEntry);
    public Task<Result>  DeleteAsync(JournalEntry journalEntry);
}