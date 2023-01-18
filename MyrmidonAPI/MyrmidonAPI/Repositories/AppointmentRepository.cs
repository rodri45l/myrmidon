using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly MyrmidonContext _myrmidonContext;

    public AppointmentRepository(MyrmidonContext myrmidonContext)
    {
        _myrmidonContext = myrmidonContext;
    }

    public async Task<ServiceResponse<Appointment>> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<Appointment>();
        var appointment = await _myrmidonContext.Appointments.FindAsync(id);

        if (appointment == null) serviceResponse.Success = false;
        else serviceResponse.Data = appointment;
        
        return serviceResponse;
    }

    public ServiceResponse<IEnumerable<Appointment>> GetAllByUserIdAsync(Guid userId)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Appointment>>();
        var appointments = _myrmidonContext.Appointments
            .Include(a => a.Users)
            .Where(a => a.Users.Any(u => u.Id == userId))
            .ToList();
        if (appointments.IsNullOrEmpty()) serviceResponse.Success = false;
        else serviceResponse.Data = appointments;
        return serviceResponse;
    }

    public async Task<Result> AddAsync(Appointment appointment)
    {
        var result = new Result();
        try
        {
            await _myrmidonContext.Appointments.AddAsync(appointment);
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

    public async Task<Result> UpdateAsync(Appointment appointment)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Appointments.Update(appointment);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
            result.Success = false;

        }

        return result;
    }

    public async Task<Result> DeleteAsync(Appointment appointment)
    {
        var result = new Result();
        try
        {
            _myrmidonContext.Appointments.Remove(appointment);
            await _myrmidonContext.SaveChangesAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
            result.Success = false;

        }

        return result;
    }
}