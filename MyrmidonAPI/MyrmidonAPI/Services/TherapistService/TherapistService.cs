using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Therapist;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.TherapistService;

public class TherapistService
{
    private readonly IMapper _mapper;
    private readonly ITherapistRepository _therapistRepository;

    public TherapistService(IMapper mapper, ITherapistRepository therapistRepository)
    {
        _mapper = mapper;
        _therapistRepository = therapistRepository; 
    }

    public async Task<ServiceResponse<IActionResult>> AddTherapist(AddTherapistDto therapistDto)
    {
        var therapist = new Therapist();

        _mapper.Map(therapistDto, therapist);
        var result =  await _therapistRepository.AddAsync(therapist);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Therapist created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemoveTherapist(Guid therapistId)
    {
        var therapist = await _therapistRepository.GetByIdAsync(therapistId);
        if (!therapist.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _therapistRepository.DeleteAsync(therapist.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Therapist deleted")
        };
    }

    public async Task<ServiceResponse<IActionResult>> UpdateTherapist(Therapist therapist)
    {
        var result =  await _therapistRepository.UpdateAsync(therapist);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Therapist updated") };
    }

    public async Task<ServiceResponse<IActionResult>> GetTherapist(Guid therapistId)
    {
        var therapist = await _therapistRepository.GetByIdAsync(therapistId);
        if (!therapist.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(therapist)
        };
    }
}