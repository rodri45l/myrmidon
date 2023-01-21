using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Patient;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.PatientService;

public interface IPatientService
{
    Task<ServiceResponse<IActionResult>> AddPatient(AddPatientDto patientDto);
    Task<ServiceResponse<IActionResult>> RemovePatient(Guid patientId);
    Task<ServiceResponse<IActionResult>> UpdatePatient(Patient patient);
    Task<ServiceResponse<IActionResult>> GetPatient(Guid patientId);
}