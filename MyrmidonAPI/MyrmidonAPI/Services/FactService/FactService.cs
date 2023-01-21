using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Fact;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.FactService;

public class FactService : IFactService
{
    private readonly IFactRepository _factRepository;
    private readonly IMapper _mapper;

    public FactService(IFactRepository factRepository, IMapper mapper)
    {
        _factRepository = factRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<IActionResult>> AddFact(AddFactDto factDto)
    {
        
        var fact = new Fact();

        _mapper.Map(factDto, fact);
      var result =  await _factRepository.AddAsync(fact);
     
      if (!result.Success) return new ServiceResponse<IActionResult>()
      {
          Success = false, Data =new BadRequestObjectResult("Something went wrong")
      };
      return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Fact created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemoveFact(int factId)
    {
        var fact = await _factRepository.GetByIdAsync(factId);
        if (!fact.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _factRepository.DeleteAsync(fact.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Fact deleted")
        };
    }
    

    public async Task<ServiceResponse<IActionResult>> UpdateFact(Fact fact)
    {
        var result = await _factRepository.UpdateAsync(fact);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Fact Updated")
        };
    }

    public async Task<ServiceResponse<IActionResult>> GetFact(int factId)
    {
        var fact = await _factRepository.GetByIdAsync(factId);
        if (!fact.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(fact)
        };
    }
}