using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Repositories.Interfaces;

public interface IAppointmentRepository
{
    public Task<ServiceResponse<Appointment>> GetByIdAsync(int id);
    public ServiceResponse<IEnumerable<Appointment>> GetAllByUserIdAsync(Guid userId);
    public Task<Result>  AddAsync(Appointment appointment);
    public Task<Result>  UpdateAsync(Appointment appointment);
    public Task<Result>  DeleteAsync(Appointment appointment);

}