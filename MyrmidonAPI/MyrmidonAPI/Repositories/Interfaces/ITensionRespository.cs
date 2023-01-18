using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface ITensionRepository
{
    public Task<ServiceResponse<Tension>> GetByIdAsync(int id);
    public Task<ServiceResponse<IEnumerable<Tension>>> GetAllAsync(Guid userId);
    public Task<Result>  AddAsync(Tension tension);
    public Task<Result>  UpdateAsync(Tension tension);
    public Task<Result>  DeleteAsync(Tension tension);
}