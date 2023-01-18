using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class PatientRepository : IPatientRepository
{
    public async Task<ServiceResponse<Patient>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IEnumerable<Patient>>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddAsync(Patient patient)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateAsync(Patient patient)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(Patient patient)
    {
        throw new NotImplementedException();
    }
}