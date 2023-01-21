using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Mood;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.MoodService;

public class MoodService : IMoodService
{
    private readonly IMapper _mapper;
    private readonly IMoodRepository _moodRepository;

    public MoodService(IMapper mapper, IMoodRepository moodRepository)
    {
        _mapper = mapper;
        _moodRepository = moodRepository; 
    }

    public async Task<ServiceResponse<IActionResult>> AddMood(AddMoodDto moodDto)
    {
        var mood = new Mood();

        _mapper.Map(moodDto, mood);
        var result =  await _moodRepository.AddAsync(mood);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
            ,Message = result.Error
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Mood created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemoveMood(int moodId)
    {
        var mood = await _moodRepository.GetByIdAsync(moodId);
        if (!mood.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _moodRepository.DeleteAsync(mood.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("Mood deleted")
        };
    }

    public async Task<ServiceResponse<IActionResult>> UpdateMood(Mood mood)
    {
        var result =  await _moodRepository.UpdateAsync(mood);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("Mood updated") };
    }

    public async Task<ServiceResponse<IActionResult>> GetMood(int moodId)
    {
        var mood = await _moodRepository.GetByIdAsync(moodId);
        if (!mood.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(mood)
        };
    }
}