using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Tension;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.TensionService;

public class TensionService : ITensionService
{
    private readonly IMapper _mapper;
    private readonly ITensionRepository _tensionRepository;

    public TensionService(IMapper mapper, ITensionRepository tensionRepository)
    {
        _mapper = mapper;
        _tensionRepository = tensionRepository; 
    }

    public async Task<ServiceResponse<IActionResult>> AddTension(AddTensionDto tensionDto)
    {
        var tension = new Tension();

        _mapper.Map(tensionDto, tension);
        var result =  await _tensionRepository.AddAsync(tension);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Tension created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemoveTension(int tensionId)
    {
        var tension = await _tensionRepository.GetByIdAsync(tensionId);
        if (!tension.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _tensionRepository.DeleteAsync(tension.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Tension deleted")
        };
    }

    public async Task<ServiceResponse<IActionResult>> UpdateTension(Tension tension)
    {
        var result =  await _tensionRepository.UpdateAsync(tension);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Tension updated") };
    }

    public async Task<ServiceResponse<IActionResult>> GetTension(int tensionId)
    {
        var tension = await _tensionRepository.GetByIdAsync(tensionId);
        if (!tension.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(tension)
        };
    }
}