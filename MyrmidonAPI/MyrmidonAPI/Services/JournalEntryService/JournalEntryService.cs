using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.JournalEntry;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories.Interfaces;

namespace MyrmidonAPI.Services.JournalEntryService;

public class JournalEntryService : IJournalEntryService
{
    private readonly IMapper _mapper;
    private readonly IJournalEntryRepository _journalEntryRepository;

    public JournalEntryService(IMapper mapper, IJournalEntryRepository journalEntryRepository)
    {
        _mapper = mapper;
        _journalEntryRepository = journalEntryRepository; 
    }

    public async Task<ServiceResponse<IActionResult>> AddJournalEntry(AddJournalEntryDto journalEntryDto)
    {
        var journalEntry = new JournalEntry();

        _mapper.Map(journalEntryDto, journalEntry);
        var result =  await _journalEntryRepository.AddAsync(journalEntry);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("JournalEntry created") };
    }

    public async Task<ServiceResponse<IActionResult>> RemoveJournalEntry(int journalEntryId)
    {
        var journalEntry = await _journalEntryRepository.GetByIdAsync(journalEntryId);
        if (!journalEntry.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        var result = await _journalEntryRepository.DeleteAsync(journalEntry.Data);
        if (!result.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new BadRequestResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult("JournalEntry deleted")
        };
    }

    public async Task<ServiceResponse<IActionResult>> UpdateJournalEntry(JournalEntry journalEntry)
    {
        var result =  await _journalEntryRepository.UpdateAsync(journalEntry);
     
        if (!result.Success) return new ServiceResponse<IActionResult>()
        {
            Success = false, Data =new BadRequestObjectResult("Something went wrong")
        };
        return new ServiceResponse<IActionResult>() { Data = new OkObjectResult("JournalEntry updated") };
    }

    public async Task<ServiceResponse<IActionResult>> GetJournalEntry(int journalEntryId)
    {
        var journalEntry = await _journalEntryRepository.GetByIdAsync(journalEntryId);
        if (!journalEntry.Success)
            return new ServiceResponse<IActionResult>()
            {
                Success = false,
                Data = new NotFoundResult()
            };
        return new ServiceResponse<IActionResult>()
        {
            Data = new OkObjectResult(journalEntry)
        };
    }
}