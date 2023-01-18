using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IMoodRepository
{
    public Task<ServiceResponse<Mood>> GetByIdAsync(int id);
    public Task<ServiceResponse<IEnumerable<Mood>>> GetAllByUserIdAsync(Guid userId);
    public Task<Result>  AddAsync(Mood mood);
    public Task<Result>  UpdateAsync(Mood mood);
    public Task<Result>  DeleteAsync(Mood mood);
}