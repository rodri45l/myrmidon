using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface ITherapistRepository
{
    public Task<ServiceResponse<Therapist>> GetByIdAsync(int id);
    public Task<ServiceResponse<IEnumerable<Therapist>>> GetAllByUserIdAsync(Guid userId);
    public Task<Result>  AddAsync(Therapist therapist);
    public Task<Result>  UpdateAsync(Therapist therapist);
    public Task<Result>  DeleteAsync(Therapist therapist);
}