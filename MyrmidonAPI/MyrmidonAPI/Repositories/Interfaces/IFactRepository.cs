using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IFactRepository
{
    public Task<ServiceResponse<Fact>> GetByIdAsync(int id);
    public Task<ServiceResponse<IEnumerable<Fact>>> GetAllAsync();
    public Task<Result>  AddAsync(Fact fact);
    public Task<Result>  UpdateAsync(Fact fact);
    public Task<Result>  DeleteAsync(Fact fact);

}