using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class TherapistRepository : ITherapistRepository
{
    public async Task<ServiceResponse<Therapist>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IEnumerable<Therapist>>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddAsync(Therapist therapist)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateAsync(Therapist therapist)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(Therapist therapist)
    {
        throw new NotImplementedException();
    }
}