using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Patient;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.PatientService;

public class PatientService
{
     private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public PatientService(IMapper mapper, IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository; 
    }

    public async Task<ServiceResponse<IActionResult>> AddPatient(AddPatientDto patientDto)
    {
        var patient = new Patient();

        _mapper.Map(patientDto, patient);
        var result =  await _patientRepository.AddAsync(patient);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Patient created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemovePatient(Guid patientId)
    {
        var patient = await _patientRepository.GetByIdAsync(patientId);
        if (!patient.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _patientRepository.DeleteAsync(patient.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Patient deleted")
        };
    }

    public async Task<ServiceResponse<IActionResult>> UpdatePatient(Patient patient)
    {
        var result =  await _patientRepository.UpdateAsync(patient);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Patient updated") };
    }

    public async Task<ServiceResponse<IActionResult>> GetPatient(Guid patientId)
    {
        var patient = await _patientRepository.GetByIdAsync(patientId);
        if (!patient.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(patient)
        };
    }
}