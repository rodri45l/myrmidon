using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IPatientRepository 
{
    public Task<ServiceResponse<Patient>> GetByIdAsync(int id);
    public Task<ServiceResponse<IEnumerable<Patient>>> GetAllAsync(Guid userId);
    public Task<Result>  AddAsync(Patient patient);
    public Task<Result>  UpdateAsync(Patient patient);
    public Task<Result>  DeleteAsync(Patient patient);
    
}