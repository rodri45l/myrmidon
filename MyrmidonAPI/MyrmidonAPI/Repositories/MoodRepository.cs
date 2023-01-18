using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class MoodRepository : IMoodRepository
{
    public async Task<ServiceResponse<Mood>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IEnumerable<Mood>>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddAsync(Mood mood)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateAsync(Mood mood)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(Mood mood)
    {
        throw new NotImplementedException();
    }
}