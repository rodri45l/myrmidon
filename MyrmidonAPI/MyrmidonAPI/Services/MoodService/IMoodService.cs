using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Mood;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.MoodService;

public interface IMoodService
{
    Task<ServiceResponse<IActionResult>> AddMood(AddMoodDto moodDto);
    Task<ServiceResponse<IActionResult>> RemoveMood(int moodId);
    Task<ServiceResponse<IActionResult>> UpdateMood(Mood mood);
    Task<ServiceResponse<IActionResult>> GetMood(int moodId);
}