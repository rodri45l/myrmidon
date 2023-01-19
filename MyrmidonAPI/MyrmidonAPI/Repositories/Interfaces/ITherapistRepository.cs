using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface ITherapistRepository
{
    public Task<ServiceResponse<Therapist>> GetByIdAsync(Guid id);
    public Task<Result> AddAsync(Therapist therapist);
    public Task<Result> UpdateAsync(Therapist therapist);
    public Task<Result> DeleteAsync(Therapist therapist);
}