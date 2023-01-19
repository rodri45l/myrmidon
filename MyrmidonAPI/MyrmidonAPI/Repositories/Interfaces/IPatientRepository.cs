using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IPatientRepository
{
    public Task<ServiceResponse<Patient>> GetByIdAsync(Guid id);
    public Task<Result> AddAsync(Patient patient);
    public Task<Result> UpdateAsync(Patient patient);
    public Task<Result> DeleteAsync(Patient patient);
}