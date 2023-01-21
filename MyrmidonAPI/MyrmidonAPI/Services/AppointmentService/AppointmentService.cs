using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Appointment;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.AppointmentService;

public class AppointmentService
{
     private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<IActionResult>> AddAppointment(AddAppointmentDto appointmentDto)
    {
        
        var appointment = new Appointment();

        _mapper.Map(appointmentDto, appointment);
      var result =  await _appointmentRepository.AddAsync(appointment);
     
      if (!result.Success) return new ServiceResponse<IActionResult>()
      {
          Success = false, Data =new BadRequestObjectResult("Something went wrong")
      };
      return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Appointment created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemoveAppointment(int appointmentId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (!appointment.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _appointmentRepository.DeleteAsync(appointment.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Appointment deleted")
        };
    }
    

    public async Task<ServiceResponse<IActionResult>> UpdateAppointment(Appointment appointment)
    {
        var result = await _appointmentRepository.UpdateAsync(appointment);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Appointment Updated")
        };
    }

    public async Task<ServiceResponse<IActionResult>> GetAppointment(int appointmentId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (!appointment.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(appointment)
        };
    }
}