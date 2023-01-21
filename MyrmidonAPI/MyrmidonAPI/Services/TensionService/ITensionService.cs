using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Tension;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.TensionService;

public interface ITensionService
{
    Task<ServiceResponse<IActionResult>> AddTension(AddTensionDto tensionDto);
    Task<ServiceResponse<IActionResult>> RemoveTension(int tensionId);
    Task<ServiceResponse<IActionResult>> UpdateTension(Tension tension);
    Task<ServiceResponse<IActionResult>> GetTension(int tensionId);
}