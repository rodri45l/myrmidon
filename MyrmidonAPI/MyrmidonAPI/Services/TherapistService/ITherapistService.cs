using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Therapist;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.TherapistService;

public interface ITherapistService
{
    Task<ServiceResponse<IActionResult>> AddTherapist(AddTherapistDto therapistDto);
    Task<ServiceResponse<IActionResult>> RemoveTherapist(Guid therapistId);
    Task<ServiceResponse<IActionResult>> UpdateTherapist(Therapist therapist);
    Task<ServiceResponse<IActionResult>> GetTherapist(Guid therapistId);
    
}