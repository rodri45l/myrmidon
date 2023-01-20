using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.Fact;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.FactService;

public interface IFactService
{
    Task<ServiceResponse<IActionResult>> AddFact(AddFactDto fact);
    Task<ServiceResponse<IActionResult>> RemoveFact(int factId);
    Task<ServiceResponse<IActionResult>> UpdateFact(Fact fact);
    Task<ServiceResponse<IActionResult>> GetFact(int factId);
    
}