using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Appointment;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.AppointmentService;

public interface IAppointmentService
{
    Task<ServiceResponse<IActionResult>> AddAppointment(AddAppointmentDto appointmentDto);
    Task<ServiceResponse<IActionResult>> RemoveAppointment(int appointmentId);
    Task<ServiceResponse<IActionResult>> UpdateAppointment(Appointment appointment);
    Task<ServiceResponse<IActionResult>> GetAppointment(int appointmentId);
    
}