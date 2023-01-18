using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ServiceResponse<User>> GetByIdAsync(Guid id);
    public Task<ServiceResponse<IEnumerable<User>>> GetAllAsync();
    public Task<Result>  AddAsync(User user);
    public Task<Result>  UpdateAsync(User user);
    public Task<Result>  DeleteAsync(User user);
}