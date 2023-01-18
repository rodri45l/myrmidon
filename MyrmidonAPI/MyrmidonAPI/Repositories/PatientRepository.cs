using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly MyrmidonContext _myrmidonContext;

    public PatientRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<Patient>> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<Patient>();
        var patient = await _myrmidonContext.Patients.FindAsync(id);

        if (patient == null) serviceResponse.Success = false;
        else serviceResponse.Data = patient;
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<Patient>>> GetAllByUserIdAsync(Guid userId)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Patient>>();
        var patients = await _myrmidonContext.Patients.Where(t => t.Id == userId).ToListAsync();

        if (patients.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = patients;
        
        return serviceResponse;
    }

    public async Task<Result> AddAsync(Patient patient)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Patients.AddAsync(patient);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(Patient patient)
    {
        var result = new Result();
        try
        {
           _myrmidonContext.Patients.Update(patient);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }

        return result;
    }

    public async Task<Result> DeleteAsync(Patient patient)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Patients.Remove(patient);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.Message;
        }

        return result;
    }
}